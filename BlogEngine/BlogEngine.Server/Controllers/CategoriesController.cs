using BlogEngine.Server.Common.Attributes;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategorySearchService _categorySearchService;

        public CategoriesController(ICategoryService categoryService, ICategorySearchService categorySearchService)
        {
            _categoryService = categoryService;
            _categorySearchService = categorySearchService;
        }

        [HttpGet(Name = "getCategories")]
        [AllowAnonymous]
        [ServiceFilter(typeof(CategoryHATEOASAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpGet("{id:int}", Name = "getCategory")]
        [AllowAnonymous]
        [ServiceFilter(typeof(CategoryHATEOASAttribute))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDTO = await _categoryService.GetByIdAsync(id);

            if (categoryDTO is null) return NotFound();

            return categoryDTO;
        }

        [HttpGet("edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryEditPageDTO))]
        public async Task<ActionResult<CategoryEditPageDTO>> PutGet(int id)
        {
            var editPageDTO = await _categoryService.GetEditPageDTOAsync(id);

            if (editPageDTO is null) return NotFound();

            return editPageDTO;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
        public async Task<ActionResult<List<CategoryDTO>>> Search([FromQuery] CategorySearchDTO categorySearchDTO)
        {
            return await _categorySearchService.SearchAsync(categorySearchDTO);
        }

        [HttpPost(Name = "createCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
        public async Task<ActionResult> Post([FromBody] CategoryCreationDTO categoryCreationDTO)
        {
            var categoryDTO = await _categoryService.InsertAsync(categoryCreationDTO);

            return new CreatedAtRouteResult("getCategory", new { categoryDTO.ID }, categoryDTO);
        }

        [HttpPut("{id:int}", Name = "updateCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        public async Task<ActionResult<CategoryDTO>> Put(int id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            var categoryDTO = await _categoryService.UpdateAsync(id, categoryUpdateDTO);

            if (categoryDTO is null) return NotFound();

            return categoryDTO;
        }

        [HttpDelete("{id:int}", Name = "deleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _categoryService.DeleteAsync(id);
        }
    }
}