namespace BlogEngine.Core.Data.Entities.JoiningEntities
{
    public class BlogGenre
    {
        public int BlogID { get; set; }
        public int GenreID { get; set; }

        public Blog Blog { get; set; }
        public Genre Genre { get; set; }
    }
}