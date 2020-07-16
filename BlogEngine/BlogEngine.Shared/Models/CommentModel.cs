using System;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.Models
{
    public class CommentModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "content is required")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "comment must be at least 2 and at max 300 characters long")]
        public int Content { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool Edited { get; set; }
    }
}