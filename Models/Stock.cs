using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace invenio.Models;

public class Stock
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid StockId { get; set; }
    
    [Required]
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    
    public virtual Product Product { get; set; }
    
    [Required]
    public int StockQuantity { get; set; }
    
    [Required]
    public int LowStockThreshold { get; set; }

    public int QuantityPerUnit { get; set; } = 1;

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Sku { get; set; } = Guid.NewGuid();
    
    [Required]
    [ForeignKey("Warehouse")]
    public Guid WarehouseId { get; set; }
    
    public virtual Warehouse Warehouse { get; set; }
}