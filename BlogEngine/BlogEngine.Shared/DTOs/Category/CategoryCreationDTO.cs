using BlogEngine.Shared.Validations;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.Category
{
    public class CategoryCreationDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} should not be more than 100 Characters")]
        [FirstLetterUppercase(ErrorMessage = "First letter should be uppercase")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public byte[] GeneralCover { get; set; }
    }
}