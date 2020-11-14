using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.CustomerReview
{
    public class CustomerReviewCreationDTO
    {
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        public int Rate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 200, MinimumLength = 5, ErrorMessage = "{0} must be at least 5 and at max 200 characters long")]
        public string Message { get; set; }
    }
}