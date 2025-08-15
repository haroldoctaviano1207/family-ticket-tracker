using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using HO.FamilyTicketTracker.API.Repository;
using HO.FamilyTicketTracker.API.Models.DTOs;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      try
      {
        var users = await _userRepository.GetAllAsync();

        return Ok(users.Select(x => x.ToUserModel()).ToList());
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
        var user = await _userRepository.GetAsync(id);

        if (user == null)
          return NotFound();

        return Ok(user.ToUserModel());
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserModel request)
    {
      try
      {
        var user = await _userRepository.UpdatAsync(id, request.ToUser());

        if (user == null)
          return NotFound();

        return Ok(user.ToUserModel());
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }
}
