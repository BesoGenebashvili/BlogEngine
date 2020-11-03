using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.Identity
{
    public class UserInfoDTO
    {
        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}