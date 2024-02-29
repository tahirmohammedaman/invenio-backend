using System.ComponentModel.DataAnnotations;

namespace invenio.Models.Dtos;

public class UserDto
{
    [Key] public Guid UserId { get; set; }  
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string ImagePath { get; set; }
}

public record RegisterDto
{
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Email { get; set; }
    public string? Password { get; set; }
    [Required] public Role Role { get; set; }
    public IFormFile? Image { get; set; }
    public bool SendMail { get; set; }
}

public record LoginDto
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}

public record TokenDto
{
    public string Token { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string DisplayImage { get; set; }
    public string Role { get; set; }
}