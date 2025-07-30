using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Models
{
  public enum RoleType
  {
    Parent,
    Child
  }

  public enum TicketStatus
  {
    Open = 16,
    InProgress = 17,
    PendingReview = 18,
    Completed = 19,
    Cancelled = 20
  }
}
