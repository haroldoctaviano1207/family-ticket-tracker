namespace HO.FamilyTicketTracker.API.Models
{
  public class LookUpValue
  {
    public int Id { get; set; }
    public string CategoryType { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int? SortOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
