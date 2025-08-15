namespace HO.FamilyTicketTracker.API.Models.DTOs
{
  public class CommentModel
  {
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserAvatar { get; set; } = string.Empty;
    public int TicketId { get; set; }
    public DateTime CreatedAt { get; set; }
  }

  public class CommentModelRequest
  {
    public string Content { get; set; } = string.Empty;
    public int TicketId { get; set; }
  }

  public static class CommentModelExtensions
  {
    public static CommentModel ToCommentModel(this Comment comment)
    {
      return new CommentModel
      {
        Id = comment.Id,
        Content = comment.Content,
        UserId = comment.UserId,
        UserName = $"{comment.User.FirstName} {comment.User.LastName}",
        UserAvatar = comment.User.Avatar,
        TicketId = comment.TicketId,
        CreatedAt = comment.CreatedAt
      };
    }

    public static Comment ToComment(this CommentModelRequest model, string userId)
    {
      return new Comment
      {
        Content = model.Content,
        UserId = userId,
        TicketId = model.TicketId
      };
    }
  }
}
