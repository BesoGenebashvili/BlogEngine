namespace BlogEngine.Shared.Models.JoiningModels
{
    public class BlogGenre
    {
        public int BlogID { get; set; }
        public int GenreID { get; set; }

        public Blog Blog { get; set; }
        public Genre Genre { get; set; }
    }
}