using BlogEngine.Shared.Validations;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs
{
    public class UserRegisterDTO : UserInfoDTO
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [CompareValues(nameof(Password), ErrorMessage = "Passwords does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}