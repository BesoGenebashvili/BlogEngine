using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.Comment;
using BlogEngine.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    public class CommentsController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("main/blog/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MainCommentDTO>))]
        public async Task<ActionResult<List<MainCommentDTO>>> GetBlogMainComments(int id)
        {
            return await _commentService.GetMainCommentsByBlogIdAsync(id);
        }

        [HttpGet("sub/blog/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubCommentDTO>))]
        public async Task<ActionResult<List<SubCommentDTO>>> GetBlogSubComments(int id)
        {
            return await _commentService.GetSubCommentsByBlogIdAsync(id);
        }

        [HttpGet("main/{id:int}", Name = "getMain")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCommentDTO))]
        public async Task<ActionResult<MainCommentDTO>> GetMain(int id)
        {
            var mainCommentDTO = await _commentService.GetMainCommentByIdAsync(id);

            if (mainCommentDTO is null) return NotFound();

            return mainCommentDTO;
        }

        [HttpGet("sub/{id:int}", Name = "getSub")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubCommentDTO))]
        public async Task<ActionResult<SubCommentDTO>> GetSub(int id)
        {
            var SubCommentDTO = await _commentService.GetSubCommentByIdAsync(id);

            if (SubCommentDTO is null) return NotFound();

            return SubCommentDTO;
        }

        [HttpPost("main")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainCommentDTO))]
        public async Task<ActionResult<MainCommentDTO>> PostMain(CommentCreationDTO commentCreationDTO)
        {
            if (!commentCreationDTO.IsMain) return BadRequest();

            var mainCommentDTO = await _commentService.InsertMainCommentAsync(commentCreationDTO);

            return new CreatedAtRouteResult("getMain", new { mainCommentDTO.ID }, mainCommentDTO);
        }

        [HttpPost("sub")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubCommentDTO))]
        public async Task<ActionResult<SubCommentDTO>> PostSub(CommentCreationDTO commentCreationDTO)
        {
            if (commentCreationDTO.IsMain) return BadRequest();

            var subCommentDTO = await _commentService.InsertSubCommentAsync(commentCreationDTO);

            return new CreatedAtRouteResult("getSub", new { subCommentDTO.ID }, subCommentDTO);
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Put([FromBody] CommentUpdateDTO commentUpdateDTO)
        {
            return await _commentService.UpdateCommentAsync(commentUpdateDTO);
        }

        [HttpDelete("main/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> DeleteMain(int id)
        {
            return await _commentService.DeleteMainCommentAsync(id);
        }

        [HttpDelete("sub/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> DeleteSub(int id)
        {
            return await _commentService.DeleteSubCommentAsync(id);
        }
    }
}