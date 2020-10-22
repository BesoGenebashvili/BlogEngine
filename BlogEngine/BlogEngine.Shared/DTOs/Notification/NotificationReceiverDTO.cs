namespace BlogEngine.Shared.DTOs
{
    public class NotificationReceiverDTO : ReadDataDTOBase
    {
        public int ID { get; set; }

        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
    }
}