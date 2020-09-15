using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs
{
    public class UserInfoDTO
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}