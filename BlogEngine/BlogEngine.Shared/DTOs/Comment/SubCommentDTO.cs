namespace BlogEngine.Shared.DTOs
{
    public class SubCommentDTO : ReadDataDTOBase
    {
        public int ID { get; set; }
        public int MainCommentID { get; set; }
        public int BlogID { get; set; }
        public string Body { get; set; }
    }
}