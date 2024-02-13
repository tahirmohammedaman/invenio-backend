using System.ComponentModel.DataAnnotations;
using invenio.Models.Dtos.Product;

namespace invenio.Models.Dtos;

public record StockDto
{
    [Key] public Guid StockId { get; set; }
    public ProductDto Product { get; set; }
    public WarehouseDto Warehouse { get; set; }
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public int QuantityPerUnit { get; set; }
    public Guid Sku { get; set; }
}

public record CreateStockDto
{
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int StockQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public int? QuantityPerUnit { get; set; }
}

public record UpdateStockDto
{
    public Guid? WarehouseId { get; set; } = null;
    public int? StockQuantity { get; set; } = null;
    public int? LowStockThreshold { get; set; } = null;
    public int? QuantityPerUnit { get; set; } = null;
}