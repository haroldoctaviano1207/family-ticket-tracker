namespace HO.FamilyTicketTracker.API.Models.DTOs
{
  public class TicketModel
  {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssigneeId { get; set; } = string.Empty;
    public string AssigneeName { get; set; } = string.Empty;
    public string AssigneeAvatar { get; set; } = string.Empty;
    public string CreatedById { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public int Category { get; set; }
    public int Status { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public bool IsOverdue { get; set; }
    public int CommentsCount { get; set; }
  }

  public class TicketRequestModel
  {
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AssigneeId { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Priority { get; set; }
    public int? Category { get; set; }
    public int? Status { get; set; }
    public string? PhotoUrl { get; set; }
  }

  public static class TicketModelExtensions
  {
    public static TicketModel ToTicketModel(this Ticket t)
    {
      return new TicketModel
      {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        AssigneeId = t.AssigneeId,
        AssigneeName = $"{t.Assignee.FirstName} {t.Assignee.LastName}",
        AssigneeAvatar = t.Assignee.Avatar,
        CreatedById = t.CreatedById,
        CreatedByName = $"{t.CreatedBy.FirstName} {t.CreatedBy.LastName}",
        CreatedAt = t.CreatedAt,
        DueDate = t.DueDate,
        Priority = t.Priority,
        Category = t.Category,
        Status = t.Status,
        PhotoUrl = t.PhotoUrl,
        CompletedAt = t.CompletedAt,
        ApprovedAt = t.ApprovedAt,
        IsOverdue = t.DueDate.HasValue && t.DueDate < DateTime.UtcNow && t.Status != Constants.CompletedStatus,
        CommentsCount = t.Comments.Count
      };
    }

    public static Ticket ToTicket(this TicketRequestModel request, string userId)
    {
      return new Ticket
      {
        Title = request.Title,
        Description = request.Description,
        AssigneeId = request.AssigneeId,
        CreatedById = userId,
        DueDate = request.DueDate,
        Priority = request.Priority.HasValue ? request.Priority.Value : 0,
        Category = request.Category.HasValue ? request.Category.Value : 0,
        Status = request.Status.HasValue ? request.Status.Value : 0,
        PhotoUrl = request.PhotoUrl
      };
    }

    public static Ticket ToTicket(this TicketRequestModel request, Ticket existingTicket)
    {
      return new Ticket
      {
        Title = request.Title ?? existingTicket.Title,
        Description = request.Description ?? existingTicket.Description,
        AssigneeId = request.AssigneeId ?? existingTicket.AssigneeId,
        DueDate = request.DueDate ?? existingTicket.DueDate,
        Priority = request.Priority ?? existingTicket.Priority,
        Category = request.Category ?? existingTicket.Category,
        Status = request.Status ?? existingTicket.Status,
        PhotoUrl = request.PhotoUrl ?? existingTicket.PhotoUrl
      };
    }
  }
}
