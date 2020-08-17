using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IReadingTimeEstimator _readingTimeEstimator;

        public BlogService(
            IBlogRepository blogRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IReadingTimeEstimator readingTimeEstimator)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _readingTimeEstimator = readingTimeEstimator;
        }

        public async Task<BlogDTO> GetByIdAsync(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return null;

            return ToDTO(blogEntity);
        }

        public async Task<BlogUpdateDTO> GetUpdateDTOAsync(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return null;

            return ToUpdateDTO(blogEntity);
        }

        public async Task<BlogEditPageDTO> GetEditPageDTOAsync(int id)
        {
            var blogEntity = await GetUpdateDTOAsync(id);

            if (blogEntity == null) return null;

            var allCategories = await _categoryRepository.GetAllWithReferences();

            var selectedCategories = allCategories
                .Where(c => blogEntity.CategoryIDs.Contains(c.ID))
                .ToList();

            var notSelectedCategories = allCategories
                .Where(c => !blogEntity.CategoryIDs.Contains(c.ID))
                .ToList();

            return new BlogEditPageDTO()
            {
                BlogUpdateDTO = blogEntity,
                SelectedCategoryDTOs = _mapper.Map<List<CategoryDTO>>(selectedCategories),
                NotSelectedCategoryDTOs = _mapper.Map<List<CategoryDTO>>(notSelectedCategories)
            };
        }

        public async Task<List<BlogDTO>> GetAllAsync()
        {
            var blogEntities = await _blogRepository.GetAllWithReferences();

            return _mapper.Map<List<BlogDTO>>(blogEntities.ToList());
        }

        public async Task<BlogDTO> InsertAsync(BlogCreationDTO blogCreationDTO)
        {
            var blogEntity = ToEntity(blogCreationDTO);

            blogEntity.EstimatedReadingTimeInMinutes = _readingTimeEstimator.GetEstimatedReadingTime(blogEntity.HTMLContent);

            var insertedEntity = await _blogRepository.InsertAsync(blogEntity);

            return ToDTO(insertedEntity);
        }

        public async Task<BlogDTO> UpdateAsync(int id, BlogUpdateDTO blogUpdateDTO)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return null;

            _mapper.Map(blogUpdateDTO, blogEntity);

            blogEntity.EstimatedReadingTimeInMinutes = _readingTimeEstimator.GetEstimatedReadingTime(blogUpdateDTO.HTMLContent);

            var updatedEntity = await _blogRepository.UpdateAsync(blogEntity);

            return ToDTO(updatedEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var blogEntity = await _blogRepository.GetByIdAsync(id);

            if (blogEntity == null) return false;

            return await _blogRepository.DeleteAsync(blogEntity.ID);
        }

        private Blog ToEntity(BlogCreationDTO blogCreationDTO) => _mapper.Map<Blog>(blogCreationDTO);
        private BlogDTO ToDTO(Blog blogEntity) => _mapper.Map<BlogDTO>(blogEntity);
        private BlogUpdateDTO ToUpdateDTO(Blog blogEntity) => _mapper.Map<BlogUpdateDTO>(blogEntity);
    }
}