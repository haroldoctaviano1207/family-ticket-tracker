using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using HO.FamilyTicketTracker.API.Repository;
using HO.FamilyTicketTracker.API.Models.DTOs;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class CommentController : ControllerBase
  {
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
      _commentRepository = commentRepository;
    }

    [HttpGet("ticket/{ticketId}")]
    public async Task<IActionResult> GetComments(int ticketId)
    {
      try
      {
        var comments = await _commentRepository.GetAllAsync(ticketId);

        return Ok(comments.Select(c => c.ToCommentModel()));
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
    {
      try
      {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
          return Unauthorized();

        var comment = new Comment
        {
          Content = request.Content,
          UserId = userId,
          TicketId = request.TicketId
        };

        var result = await _commentRepository.InsertAsync(comment);

        return CreatedAtAction(nameof(GetComments), new { ticketId = request.TicketId }, result.ToCommentModel());
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
      try
      {
        var comment = await _commentRepository.FindAsync(id);
        if (comment == null)
          return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        // Only allow user to delete their own comments or parents can delete any comment
        if (comment.UserId != userId && userRole != "Parent")
          return Forbid();

        await _commentRepository.DeleteAsync(id);

        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }

  public class CreateCommentRequest
  {
    public string Content { get; set; } = string.Empty;
    public int TicketId { get; set; }
  }

  public class CommentDto
  {
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserAvatar { get; set; } = string.Empty;
    public int TicketId { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
