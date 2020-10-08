using System.Collections.Generic;
using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Shared.DTOs
{
    public class UserProfileDTO
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}