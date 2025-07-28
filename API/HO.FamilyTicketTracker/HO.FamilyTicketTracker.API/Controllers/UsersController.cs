using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class UsersController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      try
      {
        var users = await _context.Users
            .Select(u => new UserDto
            {
              Id = u.Id,
              Email = u.Email ?? string.Empty,
              FirstName = u.FirstName,
              LastName = u.LastName,
              Avatar = u.Avatar,
              Role = u.Role,
              CompletedTasks = u.CompletedTasks,
              TotalPoints = u.TotalPoints,
              CreatedAt = u.CreatedAt
            })
            .ToListAsync();

        return Ok(users);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
      try
      {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
          return NotFound();

        var userDto = new UserDto
        {
          Id = user.Id,
          Email = user.Email ?? string.Empty,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Avatar = user.Avatar,
          Role = user.Role,
          CompletedTasks = user.CompletedTasks,
          TotalPoints = user.TotalPoints,
          CreatedAt = user.CreatedAt
        };

        return Ok(userDto);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
    {
      try
      {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
          return NotFound();

        if (!string.IsNullOrEmpty(request.FirstName))
          user.FirstName = request.FirstName;
        if (!string.IsNullOrEmpty(request.LastName))
          user.LastName = request.LastName;
        if (!string.IsNullOrEmpty(request.Avatar))
          user.Avatar = request.Avatar;

        await _context.SaveChangesAsync();

        var userDto = new UserDto
        {
          Id = user.Id,
          Email = user.Email ?? string.Empty,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Avatar = user.Avatar,
          Role = user.Role,
          CompletedTasks = user.CompletedTasks,
          TotalPoints = user.TotalPoints,
          CreatedAt = user.CreatedAt
        };

        return Ok(userDto);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }

  public class UpdateUserRequest
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
  }

  public class UserDto
  {
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Role { get; set; }
    public int CompletedTasks { get; set; }
    public int TotalPoints { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
