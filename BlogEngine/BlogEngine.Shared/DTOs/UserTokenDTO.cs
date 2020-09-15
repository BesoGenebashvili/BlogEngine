using System;

namespace BlogEngine.Shared.DTOs
{
    public class UserTokenDTO
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}