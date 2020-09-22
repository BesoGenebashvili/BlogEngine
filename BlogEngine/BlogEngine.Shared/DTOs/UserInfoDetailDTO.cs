using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs
{
    public class UserInfoDetailDTO : UserInfoDTO
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}