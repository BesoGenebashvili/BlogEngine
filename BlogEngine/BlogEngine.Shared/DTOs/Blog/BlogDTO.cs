using BlogEngine.Shared.DTOs.Category;
using BlogEngine.Shared.DTOs.Comment;
using BlogEngine.Shared.DTOs.Common;
using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs.Blog
{
    public class BlogDTO : ReadDataDTOBase
    {
        public int ID { get; set; }
        public int ApplicationUserID { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string HTMLContent { get; set; }
        public int EstimatedReadingTimeInMinutes { get; set; }
        public byte[] Cover { get; set; }
        public double AverageRating { get; set; }
        public int RatingByUser { get; set; }

        public List<MainCommentDTO> MainCommentDTOs { get; set; } = new List<MainCommentDTO>();
        public List<CategoryDTO> CategoryDTOs { get; set; } = new List<CategoryDTO>();
    }
}