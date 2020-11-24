using BlogEngine.Shared.Helpers;

namespace BlogEngine.Shared.DTOs.Category
{
    public class CategorySearchDTO
    {
        public string Name { get; set; }
        public SortOrder SortOrder { get; set; } = SortOrder.Descending;
        public CategoryOrderBy CategoryOrderBy { get; set; } = CategoryOrderBy.Featured;
    }
}