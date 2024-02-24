using System.ComponentModel.DataAnnotations;
using invenio.Models.Dtos.Category;

namespace invenio.Models.Dtos.Product;

public record ProductDto
{
    [Key] public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public List<string> ImagePaths { get; set; }
    public CategoryDto? Category { get; set; }
    public double Price { get; set; }
    public int? MinimumOrderQuantity { get; set; }
    public int? MaximumOrderQuantity { get; set; }
    public ICollection<SupplyDto>? Supplies { get; set; }
    public ICollection<Stock>? Stocks { get; set; }
}

public record CreateProductDto
{
    [Required] public string Name { get; set; }
    public string? Description { get; set; }
    [Required] public string ShortDescription { get; set; }
    [Required] public List<IFormFile> Images { get; set; }
    [Required] public double Price { get; set; }
    public int? MinimumOrderQuantity { get; set; } = 1;
    public int? MaximumOrderQuantity { get; set; }
    [Required] public Guid CategoryId { get; set; }
}

public record UpdateProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public List<IFormFile>? Images { get; set; }
    public double? Price { get; set; }
    public int? MinimumOrderQuantity { get; set; }
    public int? MaximumOrderQuantity { get; set; }
    public Guid? CategoryId { get; set; }
}