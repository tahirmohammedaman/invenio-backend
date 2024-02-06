using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos;
using invenio.Repositories;
using invenio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace invenio.Controllers;

[ApiController]
[Route("/api/")]
public class AuthController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;

    public AuthController(IRepositoryWrapper repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public IActionResult Register([FromForm] RegisterDto userDto)
    {
        // if (_repository.User.GetByEmail(userDto.Email) is not null)
        //     return BadRequest("User with this email already exists");

        var user = _mapper.Map<User>(userDto);
        var hash = AuthService.HashPassword(user.Password, out var salt);

        user.Password = hash;
        user.Salt = salt;

        _repository.User.CreateUser(user);
        _repository.Save();

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromForm] LoginDto loginDto)
    {
        if (_repository.User.GetByEmail(loginDto.Email) is not { } user)
            return BadRequest("User with this email does not exist");

        if (AuthService.VerifyPassword(loginDto.Password, user.Password, user.Salt))
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "invenio.com",
                audience: "invenio.com",
                claims: new List<Claim> {
                    new(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new(ClaimTypes.Role, user.Role),
                },
                expires: DateTime.Now.AddHours(12),
                signingCredentials: signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new TokenDto
            {
                Token = token
            });
        }
        
        return BadRequest("Invalid password");
    }
}