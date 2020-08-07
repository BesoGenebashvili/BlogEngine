using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogEngine.Core.Data.Entities.JoiningEntities;

namespace BlogEngine.Shared.DTOs
{
    public class BlogCreationDTO
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} should not be more than 100 Characters")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Short Description is required")]
        [StringLength(100, ErrorMessage = "Short Description should not be more than 100 Characters")]
        public string ShortDescription { get; set; }

        [DataType(DataType.Html)]
        [Required(ErrorMessage = "HTML Content is required")]
        public string HTMLContent { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "Created By should not be more than 50 Characters")]
        public string CreatedBy { get; set; }

        public byte[] Cover { get; set; }

        public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();
    }
}