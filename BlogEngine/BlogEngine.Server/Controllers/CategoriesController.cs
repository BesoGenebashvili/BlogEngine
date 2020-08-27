using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using BlogEngine.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoryDTO>))]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDTO = await _categoryService.GetByIdAsync(id);

            if (categoryDTO == null) return NotFound();

            return categoryDTO;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
        public async Task<ActionResult> Post([FromBody] CategoryCreationDTO categoryCreationDTO)
        {
            if (categoryCreationDTO == null) return BadRequest();

            var categoryDTO = await _categoryService.InsertAsync(categoryCreationDTO);

            return new CreatedAtRouteResult("GetCategory", new { categoryDTO.ID }, categoryDTO);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        public async Task<ActionResult<CategoryDTO>> Put(int id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            if (categoryUpdateDTO == null) return BadRequest();

            var categoryDTO = await _categoryService.UpdateAsync(id, categoryUpdateDTO);

            if (categoryDTO == null) return NotFound();

            return categoryDTO;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _categoryService.DeleteAsync(id);
        }
    }
}