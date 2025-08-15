namespace HO.FamilyTicketTracker.API.Models.DTOs
{
  public class LookUpModel
  {
    public List<LookUpValueModel> RoleType { get; set; }
    public List<LookUpValueModel> TicketPriority { get; set; }
    public List<LookUpValueModel> TicketCategory { get; set; }
    public List<LookUpValueModel> TicketStatus { get; set; }
  }

  public class LookUpValueModel
  {
    public int Id { get; set; }
    public string CategoryType { get; set; }
    public string Code { get; set; }
    public string DisplayName { get; set; }
    public int SortOrder { get; set; }
  }

  public static class LookUpValueExtensions
  {
    public static LookUpModel ToLookUpModel(this IEnumerable<LookUpValue> data)
    {
      if (data == null)
      {
        return new LookUpModel();
      }

      var activeLookUps = data.Where(lv => lv.IsActive).ToList();

      return new LookUpModel
      {
        RoleType = activeLookUps.Where(lv => lv.CategoryType == "RoleType").Select(x => x.ToDto()).ToList(),
        TicketPriority = activeLookUps.Where(lv => lv.CategoryType == "TicketPriority").Select(x => x.ToDto()).ToList(),
        TicketCategory = activeLookUps.Where(lv => lv.CategoryType == "TicketCategory").Select(x => x.ToDto()).ToList(),
        TicketStatus = activeLookUps.Where(lv => lv.CategoryType == "TicketStatus").Select(x => x.ToDto()).ToList()
      };
    }

    public static LookUpValueModel ToDto(this LookUpValue entity)
    {
      if (entity == null)
      {
        return null;
      }

      return new LookUpValueModel
      {
        Id = entity.Id,
        CategoryType = entity.CategoryType,
        Code = entity.Code,
        DisplayName = entity.DisplayName,
        SortOrder = entity.SortOrder ?? 0
      };
    }
  }
}
