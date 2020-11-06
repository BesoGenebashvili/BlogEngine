using BlogEngine.Shared.DTOs.Common;
using BlogEngine.Shared.DTOs.Identity;

namespace BlogEngine.Shared.DTOs.Comment
{
    public class CommentDTOBase : ReadDataDTOBase
    {
        public int ID { get; set; }
        public int BlogID { get; set; }
        public int ApplicationUserID { get; set; }
        public UserInfoDetailDTO UserInfoDetailDTO { get; set; }
        public string Body { get; set; }
    }
}