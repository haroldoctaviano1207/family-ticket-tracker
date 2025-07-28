using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HO.FamilyTicketTracker.API.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [MaxLength(10)]
        public string Avatar { get; set; } = string.Empty;

        public string Role { get; set; } = RoleType.Parent.ToString();
        
        public int CompletedTasks { get; set; } = 0;
        public int TotalPoints { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
