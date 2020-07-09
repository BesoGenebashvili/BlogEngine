namespace BlogEngine.Shared.Models.JoiningModels
{
    public class BlogComment
    {
        public int BlogID { get; set; }
        public int CommentID { get; set; }

        public Blog Blog { get; set; }
        public Comment Comment { get; set; }
    }
}