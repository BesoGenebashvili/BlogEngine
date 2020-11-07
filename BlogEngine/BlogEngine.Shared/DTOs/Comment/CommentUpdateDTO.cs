using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Shared.DTOs.Comment
{
    public class CommentUpdateDTO
    {
        public CommentUpdateDTO(string body, bool isMain, int id)
        {
            Body = body;
            IsMain = isMain;
            ID = id;
        }

        public int ID { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Comment {0} should not be more than 300 Characters")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public bool IsMain { get; set; }
    }
}