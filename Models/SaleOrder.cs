using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class SaleOrder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid SaleOrderId { get; set; }
    
    [Required]
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    
    public virtual Product Product { get; set; }
    
    [Required]
    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    
    public virtual Customer Customer { get; set; }
    
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
    public string DeliveryAddress { get; set; }
}