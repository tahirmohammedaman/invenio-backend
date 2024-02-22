using System.ComponentModel.DataAnnotations;
using invenio.Models.Dtos.Product;

namespace invenio.Models.Dtos;

public class SaleOrderDto
{
    [Key] public Guid SaleOrderId { get; set; }
    public ProductDto Product { get; set; }
    public CustomerDto Customer { get; set; }
    public WarehouseDto Warehouse { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public DateTime OrderDate { get; set; }
    public string DeliveryAddress { get; set; }
}

public class CreateSaleOrderDto
{
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public DateTime? OrderDate { get; set; }
    public string DeliveryAddress { get; set; }
}

public class UpdateSaleOrderDto
{
    public Guid? ProductId { get; set; } = null;
    public Guid? CustomerId { get; set; } = null;
    public Guid? WarehouseId { get; set; } = null;
    public int? Quantity { get; set; } = null;
    public double? Price { get; set; } = null;
    public DateTime? OrderDate { get; set; } = null;
    public string? DeliveryAddress { get; set; } = null;
}