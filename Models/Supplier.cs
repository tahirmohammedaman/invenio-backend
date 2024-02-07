using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class Supplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid SupplierId { get; set; }
    
    [Required]
    public string Name { get; set; }

    public string? LogoPath { get; set; } = "default_supplier_logo.svg";
    
    [Required]
    public string Country { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PrimaryPhoneNumber { get; set; }
    
    public string? SecondaryPhoneNumber { get; set; }
    
    public string? ManagerName { get; set; }
    
    public virtual ICollection<Supply> Supplies { get; set; }
}