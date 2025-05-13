using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ToDoListBusinessLayer;
using ToDoListDataLayer;
using ToDoListDataLayer.Dtos;
//using ToDoListDataLayer.DTOs;
using Microsoft.IdentityModel.JsonWebTokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Todo_List_API.Controllers;

[Route("api")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IConfiguration configuration;

    public UserController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> SingUp([FromBody] UserDto NewUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        NewUser.Password = HashPassword(NewUser.Password);

        int userId = await UserBusiness.SingUp(NewUser);

        if (userId <= 0)
        {
            return BadRequest("Failed To Create, Or Email already exists, email must be unique");
        }

        NewUser.UserId = userId;

        //var token = GenerateRandomToken();

        //await UserBusiness.SaveToken(token, userId);

        return Ok(NewUser);
    }
    private string GenerateRandomToken()
    {
        return Guid.NewGuid().ToString().Replace("-", "");

    }
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Compute the hash value from the UTF-8 encoded input string
            byte[] hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            // Convert the byte array to a lowercase hexadecimal string
            return BitConverter.ToString(hashedPassword).Replace("-", "").ToLower();
        }
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto user)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // hash the password
        user.Password = HashPassword(user.Password);

        var userFounded = await UserBusiness.Login(user);  

        if (userFounded == null)
        {
            return Unauthorized("Unauthorized user");
        }

        var token = GenerateToken(user);

        return Ok(token);
    }

    private object GenerateToken(LoginDto user)
    {
        var claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        //claims.Add(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
        //claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //claims.Add(new Claim(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString()));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(Convert.ToDouble(configuration["JWT:DurationInDays"])),
            signingCredentials: signInCredentials
        );

        var _token = new { token = new JwtSecurityTokenHandler().WriteToken(token) };
        return _token;
    }
}
