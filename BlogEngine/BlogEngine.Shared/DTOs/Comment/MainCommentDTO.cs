using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs.Comment
{
    public class MainCommentDTO : CommentDTOBase
    {
        public List<SubCommentDTO> SubCommentDTOs { get; set; } = new List<SubCommentDTO>();
    }
}