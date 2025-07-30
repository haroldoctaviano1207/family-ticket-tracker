using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models.DTOs;
using HO.FamilyTicketTracker.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HO.FamilyTicketTracker.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ReferenceController : ControllerBase
  {
    private readonly ILookUpRepository _lookUpRepository;
    public ReferenceController(ILookUpRepository lookUpRepository)
    {
      _lookUpRepository = lookUpRepository;
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookUpValues()
    {
      var lookUpValues = await _lookUpRepository.GetAsync();

      return Ok(lookUpValues.ToLookUpModel());
    }
  }
}
