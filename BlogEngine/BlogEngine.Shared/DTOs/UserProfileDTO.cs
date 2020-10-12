using System.Collections.Generic;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Shared.DTOs
{
    public class UserProfileDTO : UserInfoDetailDTO
    {
        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}