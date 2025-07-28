using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class CommentController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public CommentController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet("ticket/{ticketId}")]
    public async Task<IActionResult> GetComments(int ticketId)
    {
      try
      {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Where(c => c.TicketId == ticketId)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new CommentDto
            {
              Id = c.Id,
              Content = c.Content,
              UserId = c.UserId,
              UserName = $"{c.User.FirstName} {c.User.LastName}",
              UserAvatar = c.User.Avatar,
              TicketId = c.TicketId,
              CreatedAt = c.CreatedAt
            })
            .ToListAsync();

        return Ok(comments);
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

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var createdComment = await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == comment.Id);

        var commentDto = new CommentDto
        {
          Id = createdComment!.Id,
          Content = createdComment.Content,
          UserId = createdComment.UserId,
          UserName = $"{createdComment.User.FirstName} {createdComment.User.LastName}",
          UserAvatar = createdComment.User.Avatar,
          TicketId = createdComment.TicketId,
          CreatedAt = createdComment.CreatedAt
        };

        return CreatedAtAction(nameof(GetComments), new { ticketId = request.TicketId }, commentDto);
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
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
          return NotFound();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        // Only allow user to delete their own comments or parents can delete any comment
        if (comment.UserId != userId && userRole != "Parent")
          return Forbid();

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

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
