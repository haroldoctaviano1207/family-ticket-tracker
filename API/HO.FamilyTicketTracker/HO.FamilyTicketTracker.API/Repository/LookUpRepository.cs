using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HO.FamilyTicketTracker.API.Repository
{

  public interface ILookUpRepository
  {
    Task<List<LookUpValue>> GetAsync();
  }
  public class LookUpRepository : ILookUpRepository
  {
    private readonly ApplicationDbContext _context;
    public LookUpRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<LookUpValue>> GetAsync()
    {
      return await _context.LookupValues
        .Where(lv => lv.IsActive)
        .OrderBy(lv => lv.SortOrder)
        .ToListAsync();
    }
  }
}
