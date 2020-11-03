using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }
        public List<Blog> Blogs { get; set; } = new List<Blog>();

        public string FullName => $"{FirstName} {LastName}";

        public override bool Equals(object obj)
        {
            if (obj is ApplicationUser applicationUser)
            {
                return Email.Equals(applicationUser.Email) && Id.Equals(applicationUser.Id);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}