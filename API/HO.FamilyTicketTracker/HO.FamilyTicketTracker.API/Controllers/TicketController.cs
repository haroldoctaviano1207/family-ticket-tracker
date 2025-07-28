using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HO.FamilyTicketTracker.API.Services;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class TicketController : ControllerBase
  {
    private readonly TicketService _ticketService;

    public TicketController(TicketService ticketService)
    {
      _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
      try
      {
        var tickets = await _ticketService.GetAllTicketsAsync();
        var ticketDtos = tickets.Select(t => new TicketDto
        {
          Id = t.Id,
          Title = t.Title,
          Description = t.Description,
          AssigneeId = t.AssigneeId,
          AssigneeName = $"{t.Assignee.FirstName} {t.Assignee.LastName}",
          AssigneeAvatar = t.Assignee.Avatar,
          CreatedById = t.CreatedById,
          CreatedByName = $"{t.CreatedBy.FirstName} {t.CreatedBy.LastName}",
          CreatedAt = t.CreatedAt,
          DueDate = t.DueDate,
          Priority = t.Priority,
          Category = t.Category,
          Status = t.Status,
          PhotoUrl = t.PhotoUrl,
          CompletedAt = t.CompletedAt,
          ApprovedAt = t.ApprovedAt,
          IsOverdue = t.DueDate.HasValue && t.DueDate < DateTime.UtcNow && t.Status != Constants.CompletedStatus,
          CommentsCount = t.Comments.Count
        });

        return Ok(ticketDtos);
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
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket == null)
          return NotFound();

        var ticketDto = new TicketDto
        {
          Id = ticket.Id,
          Title = ticket.Title,
          Description = ticket.Description,
          AssigneeId = ticket.AssigneeId,
          AssigneeName = $"{ticket.Assignee.FirstName} {ticket.Assignee.LastName}",
          AssigneeAvatar = ticket.Assignee.Avatar,
          CreatedById = ticket.CreatedById,
          CreatedByName = $"{ticket.CreatedBy.FirstName} {ticket.CreatedBy.LastName}",
          CreatedAt = ticket.CreatedAt,
          DueDate = ticket.DueDate,
          Priority = ticket.Priority,
          Category = ticket.Category,
          Status = ticket.Status,
          PhotoUrl = ticket.PhotoUrl,
          CompletedAt = ticket.CompletedAt,
          ApprovedAt = ticket.ApprovedAt,
          IsOverdue = ticket.DueDate.HasValue && ticket.DueDate < DateTime.UtcNow && ticket.Status != Constants.CompletedStatus,
          CommentsCount = ticket.Comments.Count
        };

        return Ok(ticketDto);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request)
    {
      try
      {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
          return Unauthorized();

        var ticket = new Ticket
        {
          Title = request.Title,
          Description = request.Description,
          AssigneeId = request.AssigneeId,
          CreatedById = userId,
          DueDate = request.DueDate,
          Priority = request.Priority,
          Category = request.Category,
          PhotoUrl = request.PhotoUrl
        };

        var createdTicket = await _ticketService.CreateTicketAsync(ticket);
        return CreatedAtAction(nameof(GetTicket), new { id = createdTicket.Id }, createdTicket);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
    {
      try
      {
        var existingTicket = await _ticketService.GetTicketByIdAsync(id);
        if (existingTicket == null)
          return NotFound();

        var updatedTicket = new Ticket
        {
          Title = request.Title ?? existingTicket.Title,
          Description = request.Description ?? existingTicket.Description,
          AssigneeId = request.AssigneeId ?? existingTicket.AssigneeId,
          DueDate = request.DueDate ?? existingTicket.DueDate,
          Priority = request.Priority ?? existingTicket.Priority,
          Category = request.Category ?? existingTicket.Category,
          Status = request.Status ?? existingTicket.Status,
          PhotoUrl = request.PhotoUrl ?? existingTicket.PhotoUrl
        };

        var result = await _ticketService.UpdateTicketAsync(id, updatedTicket);
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
        var success = await _ticketService.DeleteTicketAsync(id);
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
        var ticket = await _ticketService.CompleteTicketAsync(id);
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
        var ticket = await _ticketService.ApproveTicketAsync(id);
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
        var ticket = await _ticketService.RejectTicketAsync(id);
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
        var tickets = await _ticketService.GetTicketsByUserIdAsync(userId);
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
        var tickets = await _ticketService.GetTicketsByStatusAsync(status);
        return Ok(tickets);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }

  public class CreateTicketRequest
  {
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssigneeId { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public int Category { get; set; }
    public string? PhotoUrl { get; set; }
  }

  public class UpdateTicketRequest
  {
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AssigneeId { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Priority { get; set; }
    public int? Category { get; set; }
    public int? Status { get; set; }
    public string? PhotoUrl { get; set; }
  }

  public class TicketDto
  {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssigneeId { get; set; } = string.Empty;
    public string AssigneeName { get; set; } = string.Empty;
    public string AssigneeAvatar { get; set; } = string.Empty;
    public string CreatedById { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public int Category { get; set; }
    public int Status { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public bool IsOverdue { get; set; }
    public int CommentsCount { get; set; }
  }
}
