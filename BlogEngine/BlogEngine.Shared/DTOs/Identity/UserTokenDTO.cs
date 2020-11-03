using System;

namespace BlogEngine.Shared.DTOs.Identity
{
    public class UserTokenDTO
    {
        public UserTokenDTO()
        {
        }

        public UserTokenDTO(string token, DateTime expirationDate)
        {
            Token = token;
            ExpirationDate = expirationDate;
        }

        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}