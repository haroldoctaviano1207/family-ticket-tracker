using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HO.FamilyTicketTracker.API.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string AssigneeId { get; set; } = string.Empty;
        
        [Required]
        public string CreatedById { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        
        [Required]
        public int Priority { get; set; }
        
        [Required]
        public int Category { get; set; }
        
        [Required]
        public int Status { get; set; }
        
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }
        
        public DateTime? CompletedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        
        [ForeignKey("AssigneeId")]
        public User Assignee { get; set; } = null!;
        
        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; } = null!;
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
