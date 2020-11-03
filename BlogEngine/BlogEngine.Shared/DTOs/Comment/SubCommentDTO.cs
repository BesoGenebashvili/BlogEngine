namespace BlogEngine.Shared.DTOs.Comment
{
    public class SubCommentDTO : ReadDataDTOBase
    {
        public int ID { get; set; }
        public int MainCommentID { get; set; }
        public int BlogID { get; set; }
        public string Body { get; set; }
    }
}