namespace BlogEngine.Shared.Models
{
    public class SMTPConfig
    {
        public string SMTPFrom { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string BCCRecipientList { get; set; }
    }
}