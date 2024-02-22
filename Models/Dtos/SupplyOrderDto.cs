using System.ComponentModel.DataAnnotations;

namespace invenio.Models.Dtos;

public record SupplyOrderDto
{
    [Key] public Guid SupplyOrderId { get; set; }
    public SupplyDto Supply { get; set; }
    public WarehouseDto Warehouse { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool IsDelivered { get; set; }
}

public record CreateSupplyOrderDto
{
    public Guid SupplyId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public bool? IsDelivered { get; set; }
}

public record UpdateSupplyOrderDto
{
    public Guid? SupplyId { get; set; } = null;
    public Guid? WarehouseId { get; set; } = null;
    public int? Quantity { get; set; } = null;
    public double? Price { get; set; } = null;
    public DateTime? OrderDate { get; set; } = null;
    public DateTime? DeliveryDate { get; set; } = null;
    public bool? IsDelivered { get; set; } = null;
}

