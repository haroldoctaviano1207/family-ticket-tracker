using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Services
{
  public class AuthService
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _configuration = configuration;
    }

    public async Task<string> GenerateJwtToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "default");
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
          {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
        Expires = DateTime.UtcNow.AddHours(24),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }

    public async Task<(bool Success, string Token, User? User)> LoginAsync(string email, string password)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null)
      {
        return (false, string.Empty, null);
      }

      var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
      if (result.Succeeded)
      {
        var token = await GenerateJwtToken(user);
        return (true, token, user);
      }

      return (false, string.Empty, null);
    }

    public async Task<(bool Success, string Token, User? User)> RegisterAsync(string email, string password, string firstName, string lastName, string avatar, string role)
    {
      var user = new User
      {
        UserName = email,
        Email = email,
        FirstName = firstName,
        LastName = lastName,
        Avatar = avatar,
        Role = role
      };

      var result = await _userManager.CreateAsync(user, password);
      if (result.Succeeded)
      {
        await _userManager.AddToRoleAsync(user, role.ToString());
        var token = await GenerateJwtToken(user);
        return (true, token, user);
      }

      return (false, string.Empty, null);
    }
  }
}
