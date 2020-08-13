using System;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.Data.Entities
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "{0} should not be more than 300 Characters")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "Created By should not be more than 30 Characters")]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdateDate { get; set; }

        public bool Edited { get; set; }

        // TODO: add rate column
    }
}