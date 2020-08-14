using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.Data.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        [StringLength(300, ErrorMessage = "Comment {0} should not be more than 300 Characters")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public bool Edited { get; set; }

        // TODO: add rate column
    }
}