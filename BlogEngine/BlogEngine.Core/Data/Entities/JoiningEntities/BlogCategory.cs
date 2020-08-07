namespace BlogEngine.Core.Data.Entities.JoiningEntities
{
    public class BlogCategory
    {
        public int BlogID { get; set; }
        public int CategoryID { get; set; }

        public Blog Blog { get; set; }
        public Category Category { get; set; }
    }
}