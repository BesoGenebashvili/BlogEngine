using System;
using AutoMapper;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using System.Linq;

namespace BlogEngine.Server.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<MainCommentDTO> GetMainCommentByIdAsync(int id)
        {
            var mainCommentEntity = await _commentRepository.GetMainCommentByIdAsync(id);

            if (mainCommentEntity == null) return null;

            return _mapper.Map<MainCommentDTO>(mainCommentEntity);
        }

        public async Task<SubCommentDTO> GetSubCommentByIdAsync(int id)
        {
            var subCommentEntity = await _commentRepository.GetSubCommentByIdAsync(id);

            if (subCommentEntity == null) return null;

            return _mapper.Map<SubCommentDTO>(subCommentEntity);
        }

        public async Task<List<MainCommentDTO>> GetMainCommentsByBlogIdAsync(int id)
        {
            var mainCommentEntities = await _commentRepository.GetMainCommentsByBlogIdAsync(id);

            return _mapper.Map<List<MainCommentDTO>>(mainCommentEntities.ToList());
        }

        public async Task<List<SubCommentDTO>> GetSubCommentsByBlogIdAsync(int id)
        {
            var subCommentEntities = await _commentRepository.GetSubCommentsByBlogIdAsync(id);

            return _mapper.Map<List<SubCommentDTO>>(subCommentEntities.ToList());
        }

        public async Task<MainCommentDTO> InsertMainCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            if (!commentCreationDTO.IsMain)
            {
                throw new InvalidOperationException("Can not insert the SubComment into MainComment Table");
            }

            var mainCommentEntity = _mapper.Map<MainComment>(commentCreationDTO);

            var insertedMainComment = await _commentRepository.InsertMainCommentAsync(mainCommentEntity);

            return _mapper.Map<MainCommentDTO>(insertedMainComment);
        }

        public async Task<SubCommentDTO> InsertSubCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            if (commentCreationDTO.IsMain)
            {
                throw new InvalidOperationException("Can not insert the MainComment into SubComment Table");
            }

            var subCommentEntity = _mapper.Map<SubComment>(commentCreationDTO);

            var insertedSubComment = await _commentRepository.InsertSubCommentAsync(subCommentEntity);

            return _mapper.Map<SubCommentDTO>(insertedSubComment);
        }

        public async Task<bool> DeleteMainCommentAsync(int id)
        {
            return await _commentRepository.DeleteMainCommentAsync(id);
        }

        public async Task<bool> DeleteSubCommentAsync(int id)
        {
            return await _commentRepository.DeleteSubCommentAsync(id);
        }
    }
}