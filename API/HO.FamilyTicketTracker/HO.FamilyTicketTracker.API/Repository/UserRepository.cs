using HO.FamilyTicketTracker.API.Controllers;
using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HO.FamilyTicketTracker.API.Repository
{
  public interface IUserRepository
  {
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetAsync(string id);
    Task<User?> UpdatAsync(string id, User user);
  }

  public class UserRepository : IUserRepository
  {
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<User?> GetAsync(string id)
    {
      return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _context.Users
            .Select(u => new User
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
    }

    public async Task<User?> UpdatAsync(string id, User updatedUser)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return null;
      }

      if (!string.IsNullOrEmpty(updatedUser.FirstName))
        user.FirstName = updatedUser.FirstName;
      if (!string.IsNullOrEmpty(updatedUser.LastName))
        user.LastName = updatedUser.LastName;
      if (!string.IsNullOrEmpty(updatedUser.Avatar))
        user.Avatar = updatedUser.Avatar;
      if (!string.IsNullOrEmpty(updatedUser.Email))
        user.Email = updatedUser.Email;
      if (!string.IsNullOrEmpty(updatedUser.Role))
        user.Role = updatedUser.Role;


      await _context.SaveChangesAsync();

      return user;
    }
  }
}
