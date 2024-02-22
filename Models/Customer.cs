using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CustomerId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? LogoPath { get; set; } = "default_customer_logo.svg";
    
    [Required]
    public string Country { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string DeliveryAddress { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string PrimaryPhoneNumber { get; set; }
    
    public string? SecondaryPhoneNumber { get; set; }
    
    public virtual ICollection<SaleOrder>? SaleOrders { get; set; }
}