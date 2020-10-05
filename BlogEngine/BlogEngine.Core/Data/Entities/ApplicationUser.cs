using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Core.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public List<Blog> Blogs { get; set; } = new List<Blog>();

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}