using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class Warehouse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid WarehouseId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Country { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    public string? Email { get; set; }
    
    public double? Latitude { get; set; }
    
    public double? Longitude { get; set; }
    
    public virtual ICollection<Stock>? Stocks { get; set; }
    
    public virtual ICollection<SupplyOrder>? SupplyOrders { get; set; }
    
    public virtual ICollection<SaleOrder>? SaleOrders { get; set; }
}