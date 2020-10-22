using BlogEngine.Shared.Helpers;
using Microsoft.Data.SqlClient;

namespace BlogEngine.Shared.DTOs
{
    public class BlogSearchDTO
    {
        public string Title { get; set; }
        public int? CategoryID { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.Descending;
        public BlogOrderBy BlogOrderBy { get; set; } = BlogOrderBy.DateCreated;
    }
}