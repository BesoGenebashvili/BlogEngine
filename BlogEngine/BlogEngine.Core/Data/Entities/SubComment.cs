namespace BlogEngine.Core.Data.Entities
{
    public class SubComment : BaseComment
    {
        public int MainCommentID { get; set; }
    }
}