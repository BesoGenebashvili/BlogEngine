using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogEngine.Shared.DTOs;
using BlogEngine.Server.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using BlogEngine.Shared.Models;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IBlogSearchService _blogSearchService;
        private readonly INotificationSender _notificationSender;

        public BlogsController(IBlogService blogService, IBlogSearchService blogSearchService, INotificationSender notificationSender)
        {
            _blogService = blogService;
            _blogSearchService = blogSearchService;
            _notificationSender = notificationSender;
        }

        //GET api/blogs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BlogDTO>))]
        public async Task<ActionResult<List<BlogDTO>>> Get()
        {
            return await _blogService.GetAllAsync();
        }

        //GET api/blogs/{id}
        [HttpGet("{id:int}", Name = "GetBlog")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogDTO))]
        public async Task<ActionResult<BlogDTO>> Get(int id)
        {
            var blogDTO = await _blogService.GetByIdAsync(id);

            if (blogDTO == null) return NotFound();

            return blogDTO;
        }

        //GET api/blogs/search
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BlogDTO>))]
        public async Task<ActionResult<List<BlogDTO>>> Search([FromQuery] BlogSearchDTO blogSearchDTO)
        {
            return await _blogSearchService.SearchAsync(blogSearchDTO);
        }

        //POST api/blogs
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BlogDTO))]
        public async Task<ActionResult> Post([FromBody] BlogCreationDTO blogCreationDTO)
        {
            if (blogCreationDTO == null) return BadRequest();

            var insertedBlog = await _blogService.InsertAsync(blogCreationDTO);

            await _notificationSender.SendBlogPostNotificationsAsync(insertedBlog);

            return new CreatedAtRouteResult("GetBlog", new { insertedBlog.ID }, insertedBlog);
        }

        //GET api/blogs/edit/{id}
        [HttpGet("edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogEditPageDTO))]
        public async Task<ActionResult<BlogEditPageDTO>> PutGet(int id)
        {
            var editPageDTO = await _blogService.GetEditPageDTOAsync(id);

            if (editPageDTO == null) return NotFound();

            return editPageDTO;
        }

        //PUT api/blogs/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogDTO))]
        public async Task<ActionResult<BlogDTO>> Put(int id, [FromBody] BlogUpdateDTO blogUpdateDTO)
        {
            if (blogUpdateDTO == null) return BadRequest();

            var blogDTO = await _blogService.UpdateAsync(id, blogUpdateDTO);

            if (blogDTO == null) return NotFound();

            return blogDTO;
        }

        //DELETE api/blogs/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _blogService.DeleteAsync(id);
        }
    }
}