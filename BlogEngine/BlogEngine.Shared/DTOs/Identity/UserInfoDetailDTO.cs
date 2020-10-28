using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs
{
    public class UserInfoDetailDTO
    {
        public int ID { get; set; }

        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public List<string> Roles { get; set; } = new List<string>();
    }
}