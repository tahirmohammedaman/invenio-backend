using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class SupplyOrder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid SupplyOrderId { get; set; }
    
    [Required]
    [ForeignKey("Supply")]
    public Guid SupplyId { get; set; }
    
    public virtual Supply Supply { get; set; }
    
    [Required]
    [ForeignKey("Warehouse")]
    public Guid WarehouseId { get; set; }
    
    public virtual Warehouse Warehouse { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [Required]
    public DateTime OrderDate { get; set; }
    
    [Required]
    public DateTime DeliveryDate { get; set; }
    
    public bool IsDelivered { get; set; } = false;
}