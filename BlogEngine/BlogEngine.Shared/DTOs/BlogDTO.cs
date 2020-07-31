using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogEngine.Core.Data.Entities.JoiningEntities;

namespace BlogEngine.Shared.DTOs
{
    public class BlogDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "title is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "title must be at least 2 and at max 50 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "short description is required")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "short description must be at least 10 and at max 100 characters long")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "content is required")]
        public string HTMLContent { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public int EstimatedReadingTimeInMinutes { get; set; }
        public byte[] Cover { get; set; }

        public List<BlogComment> BlogComments { get; set; } = new List<BlogComment>();
        public List<BlogGenre> BlogGenres { get; set; } = new List<BlogGenre>();
    }
}