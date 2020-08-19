using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs
{
    public class MainCommentDTO : ReadDataDTOBase
    {
        public int ID { get; set; }
        public int BlogID { get; set; }
        public string Body { get; set; }
        public List<SubCommentDTO> SubCommentDTOs { get; set; } = new List<SubCommentDTO>();
    }
}