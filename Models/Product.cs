using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
    public IList<string> Images { get; set; }
    
    [Required]
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
    
    public virtual Category Category { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    public int MinimumOrderQuantity { get; set; } = 1;
    
    public int? MaximumOrderQuantity { get; set; }
}