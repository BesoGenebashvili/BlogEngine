﻿using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.Comment;
using BlogEngine.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    public class CommentsController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("main/blog/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MainCommentDTO>))]
        public async Task<ActionResult<List<MainCommentDTO>>> GetBlogMainComments(int id)
        {
            return await _commentService.GetMainCommentsByBlogIdAsync(id);
        }

        [HttpGet("sub/blog/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubCommentDTO>))]
        public async Task<ActionResult<List<SubCommentDTO>>> GetBlogSubComments(int id)
        {
            return await _commentService.GetSubCommentsByBlogIdAsync(id);
        }

        [HttpGet("main/{id:int}", Name = "getMain")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCommentDTO))]
        public async Task<ActionResult<MainCommentDTO>> GetMain(int id)
        {
            var mainCommentDTO = await _commentService.GetMainCommentByIdAsync(id);

            if (mainCommentDTO == null) return NotFound();

            return mainCommentDTO;
        }

        [HttpGet("sub/{id:int}", Name = "getSub")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubCommentDTO))]
        public async Task<ActionResult<SubCommentDTO>> GetSub(int id)
        {
            var SubCommentDTO = await _commentService.GetSubCommentByIdAsync(id);

            if (SubCommentDTO == null) return NotFound();

            return SubCommentDTO;
        }

        [HttpPost("main")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainCommentDTO))]
        public async Task<ActionResult<MainCommentDTO>> PostMain(CommentCreationDTO commentCreationDTO)
        {
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubCommentDTO))]
        public async Task<ActionResult<SubCommentDTO>> PostSub(CommentCreationDTO commentCreationDTO)
        {
            if (commentCreationDTO.IsMain)
            {
                return BadRequest();
            }
            else
            {
                var subCommentDTO = await _commentService.InsertSubCommentAsync(commentCreationDTO);
                return new CreatedAtRouteResult("getSub", new { subCommentDTO.ID }, subCommentDTO);
            }
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