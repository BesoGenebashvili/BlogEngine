namespace BlogEngine.Server.Common.Models
{
    public class AccountOperationResult
    {
        public bool Successed { get; set; }
        public bool UserNotFound { get; set; }
        public string Errors { get; set; }
    }
}