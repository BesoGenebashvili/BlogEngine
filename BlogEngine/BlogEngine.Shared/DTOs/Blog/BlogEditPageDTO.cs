﻿using BlogEngine.Shared.DTOs.Category;
using System.Collections.Generic;

namespace BlogEngine.Shared.DTOs.Blog
{
    public class BlogEditPageDTO
    {
        public BlogUpdateDTO BlogUpdateDTO { get; set; }
        public List<CategoryDTO> SelectedCategoryDTOs { get; set; } = new List<CategoryDTO>();
        public List<CategoryDTO> AllCategoryDTOs { get; set; } = new List<CategoryDTO>();
    }
}