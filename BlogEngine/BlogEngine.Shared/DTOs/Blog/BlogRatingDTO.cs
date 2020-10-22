namespace BlogEngine.Shared.DTOs
{
    public class BlogRatingDTO
    {
        public BlogRatingDTO(int blogId, int rate)
        {
            BlogID = blogId;
            Rate = rate;
        }

        public int BlogID { get; set; }
        public int Rate { get; set; }
    }
}