using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ProductId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string ShortDescription { get; set; }

    public string? Description { get; set; }
    
    [Required]
    public List<string> ImagePaths { get; set; } = [];
    
    [Required]
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
    
    public virtual Category Category { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    public int MinimumOrderQuantity { get; set; } = 1;
    
    public int? MaximumOrderQuantity { get; set; }
    
    public virtual ICollection<Supply>? Supplies { get; set; }
    
    public virtual ICollection<Stock>? Stocks { get; set; }
    
    public virtual ICollection<SaleOrder>? SaleOrders { get; set; }
}