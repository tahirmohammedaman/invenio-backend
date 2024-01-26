namespace invenio.Models.Dtos.Product;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsApproved { get; set; }
}