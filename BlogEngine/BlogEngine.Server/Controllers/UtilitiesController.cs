using BlogEngine.Server.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    public class UtilitiesController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IPDFGenerator _PDFGeneratorService;

        public UtilitiesController(IBlogService blogService, IPDFGenerator pDFGeneratorService)
        {
            _blogService = blogService;
            _PDFGeneratorService = pDFGeneratorService;
        }

        [HttpGet("pdf/blog/{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        public async Task<ActionResult> Get(int id)
        {
            var blogDTO = await _blogService.GetByIdAsync(id);

            if (blogDTO is null) return NotFound();

            var pdfFile = await _PDFGeneratorService.GeneratePDFAsync(blogDTO.HTMLContent);

            return File(pdfFile, "application/pdf", $"{blogDTO.Title}.pdf");
        }
    }
}