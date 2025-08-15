using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using HO.FamilyTicketTracker.API.Models.DTOs;
using HO.FamilyTicketTracker.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ReferenceController : ControllerBase
  {
    private readonly ILookUpRepository _lookUpRepository;
    private readonly IDistributedCache _cache;
    public ReferenceController(ILookUpRepository lookUpRepository, IDistributedCache cache)
    {
      _lookUpRepository = lookUpRepository;
      _cache = cache;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookUpValues()
    {
      var cachedData = await _cache.GetStringAsync(Constants.CachedLookUpValuesKey);

      if (!string.IsNullOrEmpty(cachedData))
        return Ok(JsonSerializer.Deserialize<LookUpModel>(cachedData));
      else
      {
        var lookUpValues = await _lookUpRepository.GetAsync();
        var lookUpModels = lookUpValues.ToLookUpModel();

        var serializedData = JsonSerializer.Serialize(lookUpModels);
        await _cache.SetStringAsync(Constants.CachedLookUpValuesKey, serializedData, new DistributedCacheEntryOptions
        {
          AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        });

        return Ok(lookUpModels);
      }
    }
  }
}
