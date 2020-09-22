namespace BlogEngine.Server.Helpers
{
    public class AccountOperationResult
    {
        public bool Successed { get; set; }
        public bool UserNotFound { get; set; }
        public string Errors { get; set; }
    }
}