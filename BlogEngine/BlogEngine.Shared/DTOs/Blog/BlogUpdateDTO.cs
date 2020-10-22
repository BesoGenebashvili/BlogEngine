using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs
{
    public class BlogUpdateDTO
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(50, ErrorMessage = "{0} should not be more than 100 Characters")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Short Description is required")]
        [MaxLength(100, ErrorMessage = "Short Description should not be more than 100 Characters")]
        public string ShortDescription { get; set; }

        [DataType(DataType.Html)]
        [Required(ErrorMessage = "HTML Content is required")]
        public string HTMLContent { get; set; }

        public byte[] Cover { get; set; }
        public List<int> CategoryIDs { get; set; } = new List<int>();
    }
}