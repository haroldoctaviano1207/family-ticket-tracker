using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class UserController : ControllerBase
  {
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
      _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      var users = await _userManager.Users.Select(u => new
      {
        u.Id,
        u.Email,
        u.FirstName,
        u.LastName,
        u.Avatar,
        u.Role,
        u.CompletedTasks,
        u.TotalPoints,
        u.CreatedAt
      }).ToListAsync();

      return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user == null)
      {
        return NotFound(new { message = "User not found" });
      }

      return Ok(new
      {
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName,
        user.Avatar,
        user.Role,
        user.CompletedTasks,
        user.TotalPoints,
        user.CreatedAt
      });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Parent")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user == null)
      {
        return NotFound(new { message = "User not found" });
      }

      if (!string.IsNullOrEmpty(updateUserDto.FirstName))
        user.FirstName = updateUserDto.FirstName;

      if (!string.IsNullOrEmpty(updateUserDto.LastName))
        user.LastName = updateUserDto.LastName;

      if (!string.IsNullOrEmpty(updateUserDto.Avatar))
        user.Avatar = updateUserDto.Avatar;

      var result = await _userManager.UpdateAsync(user);
      if (!result.Succeeded)
      {
        return BadRequest(new { message = "Failed to update user" });
      }

      return Ok(new
      {
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName,
        user.Avatar,
        user.Role,
        user.CompletedTasks,
        user.TotalPoints,
        user.CreatedAt
      });
    }
  }

  public class UpdateUserDto
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Avatar { get; set; }
  }
}
