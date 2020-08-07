using System;
using System.Collections.Generic;
using BlogEngine.Core.Data.Entities.JoiningEntities;

namespace BlogEngine.Shared.DTOs
{
    public class BlogDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string HTMLContent { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public int EstimatedReadingTimeInMinutes { get; set; }
        public byte[] Cover { get; set; }

        public List<BlogComment> BlogComments { get; set; } = new List<BlogComment>();
        public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();
    }
}