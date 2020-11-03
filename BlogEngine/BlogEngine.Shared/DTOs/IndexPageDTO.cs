using BlogEngine.Shared.DTOs.Blog;
using BlogEngine.Shared.DTOs.Category;
using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs
{
    public class IndexPageDTO
    {
        public List<BlogDTO> NewBlogDTOs { get; set; } = new List<BlogDTO>();
        public List<CategoryDTO> FeaturedCategoryDTOs { get; set; } = new List<CategoryDTO>();
    }
}