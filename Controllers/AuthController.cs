using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using invenio.Models;
using invenio.Models.Dtos;
using invenio.Repositories;
using invenio.Services;
using invenio.Services.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace invenio.Controllers;

[ApiController]
[Route("/api/")]
public class AuthController : ControllerBase
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    private readonly IMailSender _mailSender;

    public AuthController(IRepositoryWrapper repository, IMapper mapper, IMailSender mailSender)
    {
        _repository = repository;
        _mapper = mapper;
        _mailSender = mailSender;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromForm] RegisterDto userDto)
    {
        try
        {
            if ((await _repository.User.GetByEmail(userDto.Email)) is not null)
                return BadRequest("User with this email already exists");

            if (userDto.Password is null)
            {
                userDto.Password = AuthService.GeneratePassword();
            }

            var user = _mapper.Map<User>(userDto);
            var hash = AuthService.HashPassword(user.Password, out var salt);

            user.Password = hash;
            user.Salt = salt;

            await _repository.User.CreateUser(user);
            await _repository.SaveAsync();
            
            // Send mail
            var mail = new Mail(
                new [] { user.Email },
                "Invenio registration",
                "Dear " + user.FirstName + " " + user.LastName
                + ",\nYour temporary password is: " + userDto.Password
            );
            _mailSender.SendMail(mail);

            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
    {
        try
        {
            var user = await _repository.User.GetByEmail(loginDto.Email);
            if (user is null)
                return BadRequest("User with this email does not exist");

            if (AuthService.VerifyPassword(loginDto.Password, user.Password, user.Salt))
            {
                var secretKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "invenio.com",
                    audience: "invenio.com",
                    claims: new List<Claim>
                    {
                        new(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                        new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new(ClaimTypes.Role, user.Role.ToString()),
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        try
        {
            var users = await _repository.User.GetAllUsers();
            var usersResponse = _mapper.Map<IEnumerable<UserDto>>(users);
            
            return Ok(usersResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, "Internal server error");
        }
    }
}