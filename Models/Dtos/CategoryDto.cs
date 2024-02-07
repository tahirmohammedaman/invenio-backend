using System.ComponentModel.DataAnnotations;
using invenio.Models.Dtos.Product;
using Microsoft.Build.Framework;

namespace invenio.Models.Dtos.Category;

public record CategoryDto
{
    [Key] public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public string? Description { get; set; }
    public CategoryDto ParentCategory { get; set; }
}

public record CreateCategoryDto
{
    public string Name { get; set; }
    public IFormFile? Image { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}

public record UpdateCategoryDto
{
    public string? Name { get; set; }
    public IFormFile? Image { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}
