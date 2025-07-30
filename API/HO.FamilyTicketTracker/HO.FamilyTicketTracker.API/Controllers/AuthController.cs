using Microsoft.AspNetCore.Mvc;
using HO.FamilyTicketTracker.API.Services;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
      _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
      try
      {
        var (success, token, user) = await _authService.LoginAsync(request.Email, request.Password);

        if (success && user != null)
        {
          return Ok(new AuthResponse
          {
            Token = token,
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Avatar = user.Avatar,
            Role = user.Role
          });
        }

        return Unauthorized(new { message = "Invalid email or password" });
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
      try
      {
        var (success, token, user) = await _authService.RegisterAsync(
            request.Email, request.Password, request.FirstName,
            request.LastName, request.Avatar, request.Role);

        if (success && user != null)
        {
          return Ok(new AuthResponse
          {
            Token = token,
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Avatar = user.Avatar,
            Role = user.Role
          });
        }

        return BadRequest(new { message = "Registration failed" });
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }

  public class LoginRequest
  {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }

  public class RegisterRequest
  {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Role { get; set; }
  }

  public class AuthResponse
  {
    public string Token { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Role { get; set; }
  }
}
