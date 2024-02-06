using System.ComponentModel.DataAnnotations;

namespace invenio.Models.Dtos;

public record WarehouseDto
{
    [Key] public Guid WarehouseId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

public record CreateWarehouseDto
{
    [Required] public string Name { get; set; }
    [Required] public string Country { get; set; }
    [Required] public string City { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

public record UpdateWarehouseDto
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}