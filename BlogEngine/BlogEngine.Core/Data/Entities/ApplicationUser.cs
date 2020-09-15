using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BlogEngine.Core.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}