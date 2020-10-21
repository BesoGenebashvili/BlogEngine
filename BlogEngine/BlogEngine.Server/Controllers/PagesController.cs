using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    public class PagesController : BaseController
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet("index")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IndexPageDTO))]
        public async Task<ActionResult<IndexPageDTO>> Get()
        {
            return await _pageService.GetIndexPageDTOAsync();
        }
    }
}