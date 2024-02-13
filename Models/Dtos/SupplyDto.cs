using System.ComponentModel.DataAnnotations;

namespace invenio.Models.Dtos;

public record SupplyDto
{
    [Key] public Guid SupplyId { get; set; }
    public Models.Product Product { get; set; }
    public Supplier Supplier { get; set; }
    public double Price { get; set; }
    public int? SupplyLeadTime { get; set; }
    public int MinimumOrderQuantity { get; set; } = 1;
    public int? MaximumOrderQuantity { get; set; }
    public bool IsDefaultSupply { get; set; } = false;
}

public record CreateSupplyDto
{
    [Required] public Guid ProductId { get; set; }
    [Required] public Guid SupplierId { get; set; }
    [Required] public double Price { get; set; }
    public int? SupplyLeadTime { get; set; }
    public int? MinimumOrderQuantity { get; set; }
    public int? MaximumOrderQuantity { get; set; }
    public bool? IsDefaultSupply { get; set; }
}

public record UpdateSupplyDto
{
    public Guid? ProductId { get; set; }
    public Guid? SupplierId { get; set; }
    public double? Price { get; set; }
    public int? SupplyLeadTime { get; set; }
    public int? MinimumOrderQuantity { get; set; }
    public int? MaximumOrderQuantity { get; set; }
    public bool? IsDefaultSupply { get; set; }
}

