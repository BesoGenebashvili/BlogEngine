using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogEngine.Shared.DTOs;
using BlogEngine.Server.Services.Abstractions;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IBlogSearchService _blogSearchService;

        public BlogsController(IBlogService blogService, IBlogSearchService blogSearchService)
        {
            _blogService = blogService;
            _blogSearchService = blogSearchService;
        }

        //GET api/blogs
        [HttpGet]
        public async Task<ActionResult<List<BlogDTO>>> Get()
        {
            return await _blogService.GetAllAsync();
        }

        //GET api/blogs/{id}
        [HttpGet("{id:int}", Name = "GetBlog")]
        public async Task<ActionResult<BlogDTO>> Get(int id)
        {
            var blogDTO = await _blogService.GetByIdAsync(id);

            if (blogDTO == null) return NotFound();

            return blogDTO;
        }

        //GET api/blogs/search
        [HttpGet("search")]
        public async Task<ActionResult<List<BlogDTO>>> Search([FromQuery] BlogSearchDTO blogSearchDTO)
        {
            return await _blogSearchService.SearchAsync(blogSearchDTO);
        }

        //POST api/blogs
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BlogCreationDTO blogCreationDTO)
        {
            if (blogCreationDTO == null) return BadRequest();

            var insertedBlog = await _blogService.InsertAsync(blogCreationDTO);

            return new CreatedAtRouteResult("GetBlog", new { insertedBlog.ID }, insertedBlog);
        }

        //GET api/blogs/edit/{id}
        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult<BlogEditPageDTO>> PutGet(int id)
        {
            var editPageDTO = await _blogService.GetEditPageDTOAsync(id);

            if (editPageDTO == null) return NotFound();

            return editPageDTO;
        }

        //PUT api/blogs/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BlogDTO>> Put(int id, [FromBody] BlogUpdateDTO blogUpdateDTO)
        {
            if (blogUpdateDTO == null) return BadRequest();

            var blogDTO = await _blogService.UpdateAsync(id, blogUpdateDTO);

            if (blogDTO == null) return NotFound();

            return blogDTO;
        }

        //DELETE api/blogs/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _blogService.DeleteAsync(id);
        }
    }
}