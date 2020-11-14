using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlogEngine.Core.Data.Entities.Common;
using BlogEngine.Core.Data.Entities.JoiningEntities;

namespace BlogEngine.Core.Data.Entities
{
    public class Blog : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        public int ApplicationUserID { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} should not be more than 50 Characters")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Short Description is required")]
        [StringLength(100, ErrorMessage = "Short Description should not be more than 100 Characters")]
        [DataType(DataType.Text)]
        public string ShortDescription { get; set; }

        [DataType(DataType.Html)]
        [Required(ErrorMessage = "HTML Content is required")]
        public string HTMLContent { get; set; }

        public int EstimatedReadingTimeInMinutes { get; set; }
        public byte[] Cover { get; set; }

        // TODO: add publish date column

        public List<MainComment> MainComments { get; set; } = new List<MainComment>();
        public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();
    }
}