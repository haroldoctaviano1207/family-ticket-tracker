using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HO.FamilyTicketTracker.API.Services;
using HO.FamilyTicketTracker.API.Models;
using HO.FamilyTicketTracker.API.Repository;
using HO.FamilyTicketTracker.API.Models.DTOs;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class TicketController : ControllerBase
  {
    private readonly ITicketRepository _ticketRepository;

    public TicketController(ITicketRepository ticketRepository)
    {
      _ticketRepository = ticketRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
      try
      {
        var tickets = await _ticketRepository.GetAllAsync();
        
        return Ok(tickets.Select(t => t.ToTicketModel()));
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicket(int id)
    {
      try
      {
        var ticket = await _ticketRepository.GetAsync(id);
        if (ticket == null)
          return NotFound();

        return Ok(ticket.ToTicketModel());
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] TicketRequestModel request)
    {
      try
      {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
          return Unauthorized();

        var createdTicket = await _ticketRepository.CreateAsync(request.ToTicket(userId));
        return CreatedAtAction(nameof(GetTicket), new { id = createdTicket.Id }, createdTicket);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, [FromBody] TicketRequestModel request)
    {
      try
      {
        var existingTicket = await _ticketRepository.GetAsync(id);
        if (existingTicket == null)
          return NotFound();

        var result = await _ticketRepository.UpdateAsync(id, request.ToTicket(existingTicket));
        if (result == null)
          return NotFound();

        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
      try
      {
        var success = await _ticketRepository.DeleteAsync(id);
        if (!success)
          return NotFound();

        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteTicket(int id)
    {
      try
      {
        var ticket = await _ticketRepository.CompleteAsync(id);
        if (ticket == null)
          return NotFound();

        return Ok(ticket);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost("{id}/approve")]
    public async Task<IActionResult> ApproveTicket(int id)
    {
      try
      {
        var ticket = await _ticketRepository.ApproveAsync(id);
        if (ticket == null)
          return NotFound();

        return Ok(ticket);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost("{id}/reject")]
    public async Task<IActionResult> RejectTicket(int id)
    {
      try
      {
        var ticket = await _ticketRepository.RejectAsync(id);
        if (ticket == null)
          return NotFound();

        return Ok(ticket);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTicketsByUser(string userId)
    {
      try
      {
        var tickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);
        return Ok(tickets);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetTicketsByStatus(int status)
    {
      try
      {
        var tickets = await _ticketRepository.GetTicketsByStatusAsync(status);
        return Ok(tickets);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }
}
