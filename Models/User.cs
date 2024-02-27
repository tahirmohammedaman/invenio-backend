using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invenio.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserId { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    [Required] public byte[] Salt { get; set; }
    
    [Required]
    public Role Role { get; set; }
}

public enum Role
{
    Basic,
    Admin
}