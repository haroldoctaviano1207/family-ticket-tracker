using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HO.FamilyTicketTracker.API.Repository
{
  public interface ICommentRepository
  {
    Task<IEnumerable<Comment>> GetAllAsync(int ticketId);
    Task<Comment> InsertAsync(Comment comment);
    Task<bool> DeleteAsync(int id);
    Task<Comment> FindAsync(int Id);
  }

  public class CommentRepository : ICommentRepository
  {
    private readonly ApplicationDbContext _context;
    public CommentRepository(ApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<Comment>> GetAllAsync(int ticketId)
    {
      return await _context.Comments
            .Include(c => c.User)
            .Where(c => c.TicketId == ticketId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Comment> FindAsync(int Id)
    {
      return await _context.Comments
          .Include(c => c.User)
          .FirstOrDefaultAsync(c => c.Id == Id);
    }
     
    public async Task<Comment> InsertAsync(Comment comment)
    {
      _context.Comments.Add(comment);
      await _context.SaveChangesAsync();

      return await _context.Comments
          .Include(c => c.User)
          .FirstOrDefaultAsync(c => c.Id == comment.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var comment = await _context.Comments.FindAsync(id);
      if (comment == null)
        return false;

      _context.Comments.Remove(comment);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}
