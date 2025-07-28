using System.ComponentModel.DataAnnotations;

namespace HO.FamilyTicketTracker.API.Models.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        [StringLength(500)]
        public string Content { get; set; } = "";

        [Required]
        public int TicketId { get; set; }
    }

    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string UserAvatar { get; set; } = "";
        public int TicketId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
