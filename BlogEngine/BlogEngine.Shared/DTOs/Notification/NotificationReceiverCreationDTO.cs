using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.Notification
{
    public class NotificationReceiverCreationDTO
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Display name is required")]
        public string DisplayName { get; set; }
    }
}