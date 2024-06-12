using CulinaryGuide.Server.HelperClasses;
using CulinaryGuide.Server.Models.Tables;
using CulinaryGuide.Server.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CulinaryGuide.Server.Controllers;

[ApiController]
[Route("auth/")]
public class AuthController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

    public AuthController(ApplicationContext context, IJwtAuthenticationManager jwtAuthenticationManager)
    {
        _context = context;
        _jwtAuthenticationManager = jwtAuthenticationManager;
    }
    
    /// <summary>
    /// Try to register a new account
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (_context.Users.Any(u => u.Username == user.Username))
        {
            return BadRequest("Username already exists.");
        }
        if (_context.Users.Any(u => u.Email == user.Email))
        {
            return BadRequest("Email already in use.");
        }

        user.Password = Encrypter.CalculateSha256(user.Password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _jwtAuthenticationManager.GenerateToken(user);
        return Ok(new { Token = token });
    }
    
    /// <summary>
    /// Try to login into an exisiting account
    /// </summary>
    /// <param name="login">Username or Email</param>
    /// <param name="password">Password</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var existingUser = _context.Users.FirstOrDefault(u => (u.Username == loginRequest.Login || u.Email == loginRequest.Login) && u.Password == Encrypter.CalculateSha256(loginRequest.Password));
        if (existingUser == null)
        {
            return Unauthorized("Invalid credentials.");
        }
        var token = _jwtAuthenticationManager.GenerateToken(existingUser);
        return Ok(new { Token = token });
    }
}