using AutoMapper;
using invenio.Models.Dtos;
using invenio.Models.Dtos.Product;

namespace invenio.Repositories.Dashboard;

public class DashboardRepository : IDashboardRepository
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public DashboardRepository(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DashboardDto> GetDashboardData()
    {
        DashboardDto dashboardData = new DashboardDto();

        // ROW 1 : Cards
        var stocks = await _repository.Stock.GetAllStocks();
        dashboardData.InventoryValue =
            Math.Round(stocks.Sum(s => s.StockQuantity * s.Product.Price), 2);
        
        dashboardData.TotalCustomers = (await _repository.Customer.GetAllCustomers()).Count();
        dashboardData.TotalProducts = (await _repository.Product.GetAllProducts()).Count();
        dashboardData.TotalSuppliers = (await _repository.Supplier.GetAllSuppliers()).Count();

        // ROW 2 : SupplyTimeline
        dashboardData.SupplyTimeline = await GetSupplyTimelineLogs();
        
        dashboardData.TotalSales = (await _repository.SaleOrder.GetAllSaleOrders())
            .Sum(order => order.Price);
        dashboardData.TotalSalesThisMonth = (await _repository.SaleOrder.GetAllSaleOrders())
            .Where(order => order.OrderDate.Month == DateTime.Now.Month)
            .Sum(order => order.Price);
        
        double totalSalesLastMonth = (await _repository.SaleOrder.GetAllSaleOrders())
            .Where(order => order.OrderDate.Month == DateTime.Now.AddMonths(-1).Month)
            .Sum(order => order.Price);

        dashboardData.TotalSalesThisMonthIncrease =
            Math.Round(((dashboardData.TotalSalesThisMonth / totalSalesLastMonth) - 1)* 100, 2);

        // ROW 3 : LowStocks and TopProducts
        stocks = await _repository.Stock.GetAllStocks();
        dashboardData.LowStocks = stocks
            .Where(stock => stock.StockQuantity * stock.QuantityPerUnit < stock.LowStockThreshold)
            .Select(stock => _mapper.Map<StockDto>(stock))
            .ToList();

        dashboardData.TopProducts = await GetTopProducts();

        // ROW 4 : TopCustomers, TopSuppliers and RecentSupplyOrders
        dashboardData.TopCustomers = await GetTopCustomers();
        dashboardData.TopSuppliers = await GetTopSuppliers();
        var supplyOrders = await _repository.SupplyOrder.GetAllSupplyOrders();
        dashboardData.RecentSupplyOrders = supplyOrders
            .OrderByDescending(order => order.OrderDate)
            .Take(5)
            .Select(order => _mapper.Map<SupplyOrderDto>(order))
            .ToList();

        return dashboardData;
    }


    private async Task<ICollection<SupplyTimeLineLog>> GetSupplyTimelineLogs()
    {
        var supplyTimelineLogs = new List<SupplyTimeLineLog>();

        var supplyOrders = await _repository.SupplyOrder.GetAllSupplyOrders();
        foreach (var order in supplyOrders)
        {
            // Add a timeline log for the order itself
            supplyTimelineLogs.Add(new SupplyTimeLineLog
                { Status = "Ordered", SupplyOrder = _mapper.Map<SupplyOrderDto>(order), DateTime = order.OrderDate });

            // If the supply order is marked as delivered, add a timeline log for it
            if (order.IsDelivered)
                supplyTimelineLogs.Add(new SupplyTimeLineLog
                {
                    Status = "Delivered", SupplyOrder = _mapper.Map<SupplyOrderDto>(order),
                    DateTime = order.DeliveryDate
                });
        }

        // Sort the supply timeline logs by DateTime in descending order
        supplyTimelineLogs = supplyTimelineLogs.OrderByDescending(log => log.DateTime).ToList();

        // Return only the top 10 logs
        return supplyTimelineLogs.Take(10).ToList();
    }

    private async Task<ICollection<TopProductDto>> GetTopProducts()
    {
        var topProducts = (await _repository.Product.GetAllProducts())
            .Where(product => product.SaleOrders != null)
            .Select(product => new TopProductDto
            {
                Product = _mapper.Map<ProductDto>(product),
                TotalSalesAmount = Math.Round(product.SaleOrders.Sum(order => order.Price), 2)
            })
            .OrderByDescending(tp => tp.TotalSalesAmount)
            .Take(5)
            .ToList();

        return topProducts;
    }

    private async Task<ICollection<TopCustomerDto>> GetTopCustomers()
    {
        var topCustomers = (await _repository.Customer.GetAllCustomers())
            .Where(customer => customer.SaleOrders != null)
            .Select(customer => new TopCustomerDto
            {
                Customer = _mapper.Map<CustomerDto>(customer),
                TotalSalesAmount = Math.Round(customer.SaleOrders.Sum(order => order.Price))
            })
            .OrderByDescending(tc => tc.TotalSalesAmount)
            .Take(5)
            .ToList();

        return topCustomers;
    }

    private async Task<ICollection<TopSupplierDto>> GetTopSuppliers()
    {
        var topSuppliers = (await _repository.Supplier.GetAllSuppliers())
            .Where(supplier => supplier.Supplies != null) // Filter out suppliers with null Supplies
            .Select(supplier => new TopSupplierDto
            {
                Supplier = _mapper.Map<SupplierDto>(supplier),
                TotalSuppliesAmount = Math.Round(supplier.Supplies
                    .Where(supply => supply.SupplyOrders != null) // Filter out supplies with null SupplyOrders
                    .SelectMany(supply => supply.SupplyOrders)
                    .Sum(order => order.Price), 2)
            })
            .OrderByDescending(ts => ts.TotalSuppliesAmount)
            .Take(5)
            .ToList();

        return topSuppliers;
    }
}