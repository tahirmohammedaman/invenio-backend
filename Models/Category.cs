using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CategoryId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [ForeignKey("ParentCategory")]
    public Guid? ParentCategoryId { get; set; }
    
    public virtual Category? ParentCategory { get; set; }
    
    public virtual ICollection<Category> ChildCategories { get; set; }
    
    public virtual ICollection<Product> Products { get; set; }
}
