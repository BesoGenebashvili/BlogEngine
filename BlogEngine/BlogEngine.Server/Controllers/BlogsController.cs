using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogEngine.Shared.DTOs;
using BlogEngine.Server.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BlogEngine.Server.Common.Extensions;
using BlogEngine.Shared.DTOs.Blog;

namespace BlogEngine.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogsController : BaseController
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

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BlogDTO>))]
        public async Task<ActionResult<List<BlogDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var blogDTOs = await _blogService.GetAllAsync();

            await HttpContext.InsertPaginationParametersInResponseAsync(blogDTOs, paginationDTO.RecordsPerPage);

            return blogDTOs.Paginate(paginationDTO).ToList();
        }

        [HttpGet("byUserId/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BlogDTO>))]
        public async Task<ActionResult<List<BlogDTO>>> GetByUserId(int id, [FromQuery] PaginationDTO paginationDTO)
        {
            var blogDTOs = await _blogService.GetAllByUserIdAsync(id);

            await HttpContext.InsertPaginationParametersInResponseAsync(blogDTOs, paginationDTO.RecordsPerPage);

            return blogDTOs.Paginate(paginationDTO).ToList();
        }

        [HttpGet("{id:int}", Name = "GetBlog")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogDTO))]
        public async Task<ActionResult<BlogDTO>> Get(int id)
        {
            var blogDTO = await _blogService.GetByIdAsync(id);

            if (blogDTO is null) return NotFound();

            return blogDTO;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BlogDTO>))]
        public async Task<ActionResult<List<BlogDTO>>> Search([FromQuery] BlogSearchDTO blogSearchDTO)
        {
            return await _blogSearchService.SearchAsync(blogSearchDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BlogDTO))]
        public async Task<ActionResult> Post([FromBody] BlogCreationDTO blogCreationDTO)
        {
            var insertedBlog = await _blogService.InsertAsync(blogCreationDTO);

            await _notificationSender.SendBlogPostNotificationsAsync(insertedBlog);

            return new CreatedAtRouteResult("GetBlog", new { insertedBlog.ID }, insertedBlog);
        }

        [HttpGet("edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogEditPageDTO))]
        public async Task<ActionResult<BlogEditPageDTO>> PutGet(int id)
        {
            var editPageDTO = await _blogService.GetEditPageDTOAsync(id);

            if (editPageDTO is null) return NotFound();

            return editPageDTO;
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogDTO))]
        public async Task<ActionResult<BlogDTO>> Put(int id, [FromBody] BlogUpdateDTO blogUpdateDTO)
        {
            var blogDTO = await _blogService.UpdateAsync(id, blogUpdateDTO);

            if (blogDTO is null) return NotFound();

            return blogDTO;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _blogService.DeleteAsync(id);
        }
    }
}