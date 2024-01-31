using invenio.Models.Dtos.Product;
using Microsoft.Build.Framework;

namespace invenio.Models.Dtos.Category;

public record CategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryDto ParentCategory { get; set; }
}

public record CreateCategoryDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}

public record UpdateCategoryDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}

// public static class CategoryDtoExtensions
// {
//     public static CategoryDto ToDto(this Models.Category category) =>
//         new()
//         {
//             CategoryId = category.CategoryId,
//             Name = category.Name,
//             Description = category.Description,
//             ParentCategory = category.ParentCategory?.ToDto()
//         };
//
//     public static Models.Category ToModel(this CreateCategoryDto category) =>
//         new()
//         {
//             Name = category.Name,
//             Description = category.Description,
//             ParentCategoryId = category.ParentCategoryId
//         };
//     
//     public static void patchModel(this UpdateCategoryDto category, Models.Category model)
//     {
//         model.Name = (category.Name is not null) ? category.Name : model.Name;
//         model.Description = (category.Description is not null) ? category.Description : model.Description;
//         model.ParentCategoryId = (category.ParentCategoryId.HasValue) ? category.ParentCategoryId : model.ParentCategoryId;
//         Console.WriteLine(">>> " + model.ParentCategoryId);
//     }
//
//     public static Models.Category ToModel(this UpdateCategoryDto category) =>
//         new()
//         {
//             Name = category.Name,
//             Description = category.Description,
//             ParentCategoryId = category.ParentCategoryId
//         };
// }
