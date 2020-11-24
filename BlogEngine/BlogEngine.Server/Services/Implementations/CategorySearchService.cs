using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.Category;
using BlogEngine.Shared.Helpers;

namespace BlogEngine.Server.Services.Implementations
{
    public class CategorySearchService : ICategorySearchService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategorySearchService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> SearchAsync(CategorySearchDTO categorySearchDTO)
        {
            Preconditions.NotNull(categorySearchDTO, nameof(categorySearchDTO));

            var categories = await _categoryRepository.GetAllWithReferences();

            if (!string.IsNullOrWhiteSpace(categorySearchDTO.Name))
            {
                categories = categories.Where(c => c.Name.Contains(categorySearchDTO.Name, StringComparison.OrdinalIgnoreCase));
            }

            categories = categorySearchDTO.SortOrder == SortOrder.Descending ?
                OrderByDescending(categories, categorySearchDTO.CategoryOrderBy) :
                OrderByAscending(categories, categorySearchDTO.CategoryOrderBy);

            return _mapper.Map<List<CategoryDTO>>(categories.ToList());
        }

        private IEnumerable<Category> OrderByAscending(IEnumerable<Category> categories, CategoryOrderBy categoryOrderBy)
        {
            return categoryOrderBy == CategoryOrderBy.Newest ?
                categories.OrderBy(c => c.DateCreated) :
                categories.OrderBy(c => c.BlogCategories.Count());
        }

        private IEnumerable<Category> OrderByDescending(IEnumerable<Category> categories, CategoryOrderBy categoryOrderBy)
        {
            return categoryOrderBy == CategoryOrderBy.Newest ?
                categories.OrderByDescending(c => c.DateCreated) :
                categories.OrderByDescending(c => c.BlogCategories.Count());
        }
    }
}