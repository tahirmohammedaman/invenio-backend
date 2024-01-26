using System.ComponentModel.DataAnnotations;

namespace invenio.Models.Dtos.Product;

public class CreateProductDto
{
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    public bool IsApproved { get; set; }
}