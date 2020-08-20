using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("main/{id}", Name = "getMain")]
        public async Task<ActionResult<MainCommentDTO>> GetMain(int id)
        {
            var mainCommentDTO = await _commentService.GetMainCommentByIdAsync(id);

            if (mainCommentDTO == null) return NotFound();

            return mainCommentDTO;
        }

        [HttpGet("sub/{id}", Name = "getSub")]
        public async Task<ActionResult<SubCommentDTO>> GetSub(int id)
        {
            var SubCommentDTO = await _commentService.GetSubCommentByIdAsync(id);

            if (SubCommentDTO == null) return NotFound();

            return SubCommentDTO;
        }

        [HttpPost("main")]
        public async Task<ActionResult<MainCommentDTO>> PostMain(CommentCreationDTO commentCreationDTO)
        {
            if (commentCreationDTO == null) return BadRequest();

            if (commentCreationDTO.IsMain)
            {
                var mainCommentDTO = await _commentService.InsertMainCommentAsync(commentCreationDTO);
                return new CreatedAtRouteResult("getMain", new { mainCommentDTO.ID }, mainCommentDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("sub")]
        public async Task<ActionResult<SubCommentDTO>> PostSub(CommentCreationDTO commentCreationDTO)
        {
            if (commentCreationDTO == null) return BadRequest();

            if(commentCreationDTO.IsMain)
            {
                return BadRequest();
            }
            else
            {
                var subCommentDTO = await _commentService.InsertSubCommentAsync(commentCreationDTO);
                return new CreatedAtRouteResult("getSub", new { subCommentDTO.ID }, subCommentDTO);
            }
        }

        [HttpDelete("main/{id}")]
        public async Task<ActionResult<bool>> DeleteMain(int id)
        {
            return await _commentService.DeleteMainCommentAsync(id);
        }

        [HttpDelete("sub/{id}")]
        public async Task<ActionResult<bool>> DeleteSub(int id)
        {
            return await _commentService.DeleteSubCommentAsync(id);
        }
    }
}