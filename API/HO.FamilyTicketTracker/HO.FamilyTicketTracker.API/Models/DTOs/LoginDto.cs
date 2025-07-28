using System.ComponentModel.DataAnnotations;

namespace HO.FamilyTicketTracker.API.Models.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }

    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        public string Avatar { get; set; } = "👤";

        [Required]
        public string Role { get; set; } = "Child";
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Avatar { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
