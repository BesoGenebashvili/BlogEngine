using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BlogEngine.Shared.DTOs.Blog;
using BlogEngine.Server.Services.Abstractions;

namespace BlogEngine.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BlogRatingsController : BaseController
    {
        private readonly IBlogRatingService _blogRatingService;

        public BlogRatingsController(IBlogRatingService blogRatingService)
        {
            _blogRatingService = blogRatingService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Post([FromBody] BlogRatingDTO blogRatingDTO)
        {
            if (blogRatingDTO is null) return BadRequest();

            await _blogRatingService.InsertAsync(blogRatingDTO);

            return NoContent();
        }
    }
}