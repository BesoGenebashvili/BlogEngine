using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.Identity
{
    public class UserUpdateDTO
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}