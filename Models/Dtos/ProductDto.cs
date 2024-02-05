using System.ComponentModel.DataAnnotations;
using invenio.Models.Dtos.Category;

namespace invenio.Models.Dtos.Product;

public record ProductDto
{
    public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public string? ShortDescription { get; set; }
    public string? Description { get; set; }
    public string Image1Path { get; set; }
    public string? Image2Path { get; set; }
    public string? Image3Path { get; set; }
    public string? Image4Path { get; set; }
    public CategoryDto? Category { get; set; }
    public double Price { get; set; }
    public int? MinimumOrderQuantity { get; set; }
    public int? MaximumOrderQuantity { get; set; }
    public ICollection<SupplyDto>? Supplies { get; set; }
    public StockDto? Stock { get; set; }
}

public record CreateProductDto
{
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public string ShortDescription { get; set; }
    [Required] public IFormFile Image1 { get; set; }
    public IFormFile? Image2 { get; set; }
    public IFormFile? Image3 { get; set; }
    public IFormFile? Image4 { get; set; }
    [Required] public double Price { get; set; }
    public int? MinimumOrderQuantity { get; set; }
    public int? MaximumOrderQuantity { get; set; }
    [Required] public Guid CategoryId { get; set; }
}

public record UpdateProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public IFormFile? Image1 { get; set; }
    public IFormFile? Image2 { get; set; }
    public IFormFile? Image3 { get; set; }
    public IFormFile? Image4 { get; set; }
    public double? Price { get; set; }
    public int? MinimumOrderQuantity { get; set; }
    public int? MaximumOrderQuantity { get; set; }
    public Guid? CategoryId { get; set; }
}