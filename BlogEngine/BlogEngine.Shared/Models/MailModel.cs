namespace BlogEngine.Shared.Models
{
    public class MailModel
    {
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}