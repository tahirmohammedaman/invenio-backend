using System.Collections;
using invenio.Models.Dtos.Product;

namespace invenio.Models.Dtos;

public class DashboardDto
{
    // ROW 1
    public double InventoryValue { get; set; }
    public int TotalProducts { get; set; }
    public int TotalCustomers { get; set; }
    public int TotalSuppliers { get; set; }
    
    // ROW 2
    public ICollection<SupplyTimeLineLog> SupplyTimeline { get; set; }
    public double TotalSales { get; set; }
    public double TotalSalesThisMonth { get; set; }
    public double TotalSalesThisMonthIncrease { get; set; }

    
    // ROW 3
    public ICollection<StockDto> LowStocks { get; set; }
    public ICollection<TopProductDto> TopProducts { get; set; }
    
    // ROW 4
    public ICollection<TopCustomerDto> TopCustomers { get; set; }
    public ICollection<TopSupplierDto> TopSuppliers { get; set; }
    public ICollection<SupplyOrderDto> RecentSupplyOrders { get; set; }
}

public record SupplyTimeLineLog
{
    public string Status { get; set; }
    public SupplyOrderDto SupplyOrder { get; set; }
    public DateTime DateTime { get; set; }
}

public record TopProductDto
{
    public ProductDto Product { get; set; }
    public double TotalSalesAmount { get; set; }
}

public record TopCustomerDto
{
    public CustomerDto Customer { get; set; }
    public double TotalSalesAmount { get; set; }
}

public record TopSupplierDto
{
    public SupplierDto Supplier { get; set; }
    public double TotalSuppliesAmount { get; set; }
}
