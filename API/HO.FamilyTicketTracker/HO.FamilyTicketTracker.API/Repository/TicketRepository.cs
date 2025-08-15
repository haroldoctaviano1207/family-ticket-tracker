using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HO.FamilyTicketTracker.API.Repository
{
  public interface ITicketRepository
  {
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<Ticket> CreateAsync(Ticket ticket);
    Task<Ticket?> UpdateAsync(int id, Ticket updatedTicket);
    Task<Ticket?> GetAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Ticket?> CompleteAsync(int id);
    Task<Ticket?> ApproveAsync(int id);
    Task<Ticket?> RejectAsync(int id);
    Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(string userId);
    Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(int status);
  }
  public class TicketRepository : ITicketRepository
  {
    private readonly ApplicationDbContext _context;

    public TicketRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
      return await _context.Tickets
          .Include(t => t.Assignee)
          .Include(t => t.CreatedBy)
          .Include(t => t.Comments)
          .OrderByDescending(t => t.CreatedAt)
          .ToListAsync();
    }

    public async Task<Ticket?> GetAsync(int id)
    {
      return await _context.Tickets
          .Include(t => t.Assignee)
          .Include(t => t.CreatedBy)
          .Include(t => t.Comments)
          .ThenInclude(c => c.User)
          .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
      _context.Tickets.Add(ticket);
      await _context.SaveChangesAsync();
      return await GetAsync(ticket.Id) ?? ticket;
    }

    public async Task<Ticket?> UpdateAsync(int id, Ticket updatedTicket)
    {
      var existingTicket = await _context.Tickets.FindAsync(id);
      if (existingTicket == null)
        return null;

      existingTicket.Title = updatedTicket.Title;
      existingTicket.Description = updatedTicket.Description;
      existingTicket.AssigneeId = updatedTicket.AssigneeId;
      existingTicket.DueDate = updatedTicket.DueDate;
      existingTicket.Priority = updatedTicket.Priority;
      existingTicket.Category = updatedTicket.Category;
      existingTicket.Status = updatedTicket.Status;
      existingTicket.PhotoUrl = updatedTicket.PhotoUrl;

      await _context.SaveChangesAsync();
      return await GetAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return false;

      _context.Tickets.Remove(ticket);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<Ticket?> CompleteAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return null;

      ticket.Status = (int)TicketStatus.PendingReview;
      ticket.CompletedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();
      return await GetAsync(id);
    }

    public async Task<Ticket?> ApproveAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return null;

      ticket.Status = (int)TicketStatus.Completed;
      ticket.ApprovedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();
      return await GetAsync(id);
    }

    public async Task<Ticket?> RejectAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return null;

      ticket.Status = (int)TicketStatus.Cancelled;
      ticket.CompletedAt = null;
      await _context.SaveChangesAsync();
      return await GetAsync(id);
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(string userId)
    {
      return await _context.Tickets
          .Include(t => t.Assignee)
          .Include(t => t.CreatedBy)
          .Include(t => t.Comments)
          .Where(t => t.AssigneeId == userId)
          .OrderByDescending(t => t.CreatedAt)
          .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(int status)
    {
      return await _context.Tickets
          .Include(t => t.Assignee)
          .Include(t => t.CreatedBy)
          .Include(t => t.Comments)
          .Where(t => t.Status == status)
          .OrderByDescending(t => t.CreatedAt)
          .ToListAsync();
    }
  }
}
