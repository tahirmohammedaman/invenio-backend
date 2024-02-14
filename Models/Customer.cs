using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace invenio.Models;

public class Customer {
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id {get; set;}

  [Required]
  public string Name {get; set;}

  [Required]
  public string Country { get; set; }

  public string City { get; set; }

  public string Address { get; set; }

  public string ManagerName { get; set; }

  public string PhoneNumber { get; set; }

  [EmailAddress]
  public string Email { get; set; }
}
