namespace invenio.Models.Dtos;

public record RegisterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
public record LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public record TokenDto
{
    public string Token { get; set; }
}