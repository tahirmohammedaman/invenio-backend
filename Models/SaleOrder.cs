using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace invenio.Models;

public class SaleOrder {
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  [ForeignKey("Product")]
  public int ProductId { get; set; }

  [Required]
  public int Amount { get; set; }

  [Required]
  [Column(TypeName = "decimal(18, 2)")]
  public decimal OrderPrice { get; set; }

  [Required]
  public DateTime Date { get; set; }
}
