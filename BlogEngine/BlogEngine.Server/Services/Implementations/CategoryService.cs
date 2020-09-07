using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
{
    public class CategoryService : DataServiceBase<Category, CategoryDTO, CategoryCreationDTO, CategoryUpdateDTO>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public override async Task<CategoryDTO> InsertAsync(CategoryCreationDTO categoryCreationDTO)
        {
            var categoryEntity = ToEntity(categoryCreationDTO);

            var insertedEntity = await _categoryRepository.InsertAsync(categoryEntity);

            return ToDTO(insertedEntity);
        }

        public override async Task<CategoryDTO> UpdateAsync(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);

            if (categoryEntity == null) return null;

            _mapper.Map(categoryUpdateDTO, categoryEntity);

            var updatedEntity = await _categoryRepository.UpdateAsync(categoryEntity);

            return ToDTO(updatedEntity);
        }

        public async Task<CategoryEditPageDTO> GetEditPageDTOAsync(int id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);

            if (categoryEntity == null) return null;

            return _mapper.Map<CategoryEditPageDTO>(categoryEntity);
        }
    }
}