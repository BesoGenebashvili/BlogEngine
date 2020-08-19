using System.Collections.Generic;
using BlogEngine.Core.Data.Entities.JoiningEntities;

namespace BlogEngine.Shared.DTOs
{
    public class BlogDTO : ReadDataDTOBase
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string HTMLContent { get; set; }
        public int EstimatedReadingTimeInMinutes { get; set; }
        public byte[] Cover { get; set; }

        public List<MainCommentDTO> MainCommentDTOs { get; set; } = new List<MainCommentDTO>();
        public List<CategoryDTO> CategoryDTOs { get; set; } = new List<CategoryDTO>();
    }
}