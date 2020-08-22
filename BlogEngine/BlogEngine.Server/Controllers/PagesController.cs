using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PagesController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet("index")]
        public async Task<ActionResult<IndexPageDTO>> Get()
        {
            return await _pageService.GetIndexPageDTOAsync();
        }
    }
}