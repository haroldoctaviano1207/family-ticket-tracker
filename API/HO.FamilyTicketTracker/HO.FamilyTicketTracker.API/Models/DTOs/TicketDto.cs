using System.ComponentModel.DataAnnotations;

namespace HO.FamilyTicketTracker.API.Models.DTOs
{
    public class CreateTicketDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = "";

        [StringLength(1000)]
        public string Description { get; set; } = "";

        [Required]
        public string AssigneeId { get; set; } = "";

        public DateTime? DueDate { get; set; }

        [Required]
        public string Priority { get; set; } = "Medium";

        [Required]
        public string Category { get; set; } = "General";

        public string? PhotoUrl { get; set; }
    }

    public class UpdateTicketDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? AssigneeId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Priority { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public string? PhotoUrl { get; set; }
    }

    public class TicketResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string AssigneeId { get; set; } = "";
        public string AssigneeName { get; set; } = "";
        public string AssigneeAvatar { get; set; } = "";
        public string CreatedById { get; set; } = "";
        public string CreatedByName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; } = "";
        public string Category { get; set; } = "";
        public string Status { get; set; } = "";
        public string? PhotoUrl { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public bool IsOverdue { get; set; }
        public int CommentsCount { get; set; }
    }
}
