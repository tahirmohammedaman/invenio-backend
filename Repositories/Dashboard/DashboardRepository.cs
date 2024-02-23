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
    
    public DashboardDto GetDashboardData()
    {
        DashboardDto dashboardData = new DashboardDto();
        
        // ROW 1 : Cards
        dashboardData.InventoryValue =
            Math.Round(_repository.Stock.GetAllStocks().Sum(s => s.StockQuantity * s.Product.Price), 2);
        dashboardData.TotalCustomers = _repository.Customer.GetAllCustomers().Count();
        dashboardData.TotalProducts = _repository.Product.GetAllProducts().Count();
        dashboardData.TotalSuppliers = _repository.Supplier.GetAllSuppliers().Count();
        
        // ROW 2 : SupplyTimeline
        dashboardData.SupplyTimeline = GetSupplyTimelineLogs();
        
        // ROW 3 : LowStocks and TopProducts
        dashboardData.LowStocks = _repository.Stock.GetAllStocks()
            .Where(stock => stock.StockQuantity * stock.QuantityPerUnit < stock.LowStockThreshold)
            .Select(stock => _mapper.Map<StockDto>(stock))
            .ToList();

        dashboardData.TopProducts = GetTopProducts();
        
        // ROW 4 : TopCustomers, TopSuppliers and RecentSupplyOrders
        dashboardData.TopCustomers = GetTopCustomers();
        dashboardData.TopSuppliers = GetTopSuppliers();
        dashboardData.RecentSupplyOrders = _repository.SupplyOrder.GetAllSupplyOrders()
            .OrderByDescending(order => order.OrderDate)
            .Take(5)
            .Select(order => _mapper.Map<SupplyOrderDto>(order))
            .ToList();
        
        return dashboardData;
    }
    
    private ICollection<SupplyTimeLineLog> GetSupplyTimelineLogs()
    {
        var supplyTimelineLogs = new List<SupplyTimeLineLog>();
        
        var supplyOrders = _repository.SupplyOrder.GetAllSupplyOrders();
        foreach (var order in supplyOrders)
        {
            // Add a timeline log for the order itself
            supplyTimelineLogs.Add(new SupplyTimeLineLog { Status = "Ordered", SupplyOrder = _mapper.Map<SupplyOrderDto>(order), DateTime = order.OrderDate });

            // If the supply order is marked as delivered, add a timeline log for it
            if (order.IsDelivered)
                supplyTimelineLogs.Add(new SupplyTimeLineLog { Status = "Delivered", SupplyOrder = _mapper.Map<SupplyOrderDto>(order), DateTime = order.DeliveryDate });
        }

        // Sort the supply timeline logs by DateTime in descending order
        supplyTimelineLogs = supplyTimelineLogs.OrderByDescending(log => log.DateTime).ToList();

        // Return only the top 10 logs
        return supplyTimelineLogs.Take(10).ToList();
    }
    
    private ICollection<TopProductDto> GetTopProducts()
    {
        var topProducts = _repository.Product.GetAllProducts()
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

    private ICollection<TopCustomerDto> GetTopCustomers()
    {
        var topCustomers = _repository.Customer.GetAllCustomers()
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

    private ICollection<TopSupplierDto> GetTopSuppliers()
    {
        var topSuppliers = _repository.Supplier.GetAllSuppliers()
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