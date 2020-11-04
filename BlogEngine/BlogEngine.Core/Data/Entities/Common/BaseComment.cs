using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.Data.Entities.Common
{
    public class BaseComment : BaseEntity
    {
        public int BlogID { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Comment {0} should not be more than 300 Characters")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        // TODO: add rate and edited column
    }
}