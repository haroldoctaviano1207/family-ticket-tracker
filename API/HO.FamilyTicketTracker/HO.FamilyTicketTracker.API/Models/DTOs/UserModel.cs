namespace HO.FamilyTicketTracker.API.Models.DTOs
{
  public class UserModel
  {
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Role { get; set; }
    public int CompletedTasks { get; set; }
    public int TotalPoints { get; set; }
    public DateTime CreatedAt { get; set; }
  }

  public static class UserModelExtensions
  {
    public static UserModel ToUserModel(this User user)
    {
      return new UserModel
      {
        Id = user.Id,
        Email = user.Email ?? string.Empty,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Avatar = user.Avatar,
        Role = user.Role,
        CompletedTasks = user.CompletedTasks,
        TotalPoints = user.TotalPoints,
        CreatedAt = user.CreatedAt
      };
    }

    public static User ToUser(this UserModel user)
    {
      return new User
      {
        Id = user.Id,
        Email = user.Email ?? string.Empty,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Avatar = user.Avatar,
        Role = user.Role,
        CompletedTasks = user.CompletedTasks,
        TotalPoints = user.TotalPoints,
        CreatedAt = user.CreatedAt
      };
    }
  }
}
