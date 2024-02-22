using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class Supply
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid SupplyId { get; set; }
    
    [Required]
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    
    public virtual Product Product { get; set; }
    
    [Required]
    [ForeignKey("Supplier")]
    public Guid SupplierId { get; set; }
    
    public virtual Supplier Supplier { get; set; }
    
    [Required]
    public double Price { get; set; }

    public int? SupplyLeadTime { get; set; } = 5;
    
    public int MinimumOrderQuantity { get; set; } = 1;
    
    public int? MaximumOrderQuantity { get; set; }

    public bool? IsDefaultSupply { get; set; } = false;
    
    public virtual ICollection<SupplyOrder>? SupplyOrders { get; set; }
}