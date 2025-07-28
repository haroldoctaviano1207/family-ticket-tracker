using Microsoft.EntityFrameworkCore;
using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Services
{
  public class TicketService
  {
    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
      return await _context.Tickets
          .Include(t => t.Assignee)
          .Include(t => t.CreatedBy)
          .Include(t => t.Comments)
          .OrderByDescending(t => t.CreatedAt)
          .ToListAsync();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
      return await _context.Tickets
          .Include(t => t.Assignee)
          .Include(t => t.CreatedBy)
          .Include(t => t.Comments)
          .ThenInclude(c => c.User)
          .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Ticket> CreateTicketAsync(Ticket ticket)
    {
      _context.Tickets.Add(ticket);
      await _context.SaveChangesAsync();
      return await GetTicketByIdAsync(ticket.Id) ?? ticket;
    }

    public async Task<Ticket?> UpdateTicketAsync(int id, Ticket updatedTicket)
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
      return await GetTicketByIdAsync(id);
    }

    public async Task<bool> DeleteTicketAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return false;

      _context.Tickets.Remove(ticket);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<Ticket?> CompleteTicketAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return null;

      ticket.Status = 3;
      ticket.CompletedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();
      return await GetTicketByIdAsync(id);
    }

    public async Task<Ticket?> ApproveTicketAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return null;

      ticket.Status = 4;
      ticket.ApprovedAt = DateTime.UtcNow;
      await _context.SaveChangesAsync();
      return await GetTicketByIdAsync(id);
    }

    public async Task<Ticket?> RejectTicketAsync(int id)
    {
      var ticket = await _context.Tickets.FindAsync(id);
      if (ticket == null)
        return null;

      ticket.Status = 2;
      ticket.CompletedAt = null;
      await _context.SaveChangesAsync();
      return await GetTicketByIdAsync(id);
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
