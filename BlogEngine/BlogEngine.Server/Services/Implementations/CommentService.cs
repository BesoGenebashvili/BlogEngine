using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.Helpers;
using BlogEngine.Shared.DTOs.Comment;

namespace BlogEngine.Server.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CommentService(
            ICommentRepository commentRepository,
            ICurrentUserProvider currentUserProvider,
            IAccountService accountService,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _currentUserProvider = currentUserProvider;
            _accountService = accountService;
        }

        public async Task<MainCommentDTO> GetMainCommentByIdAsync(int id)
        {
            var mainCommentEntity = await _commentRepository.GetMainCommentByIdAsync(id);

            if (mainCommentEntity == null) return null;

            var mainCommentDTO = _mapper.Map<MainCommentDTO>(mainCommentEntity);

            await BindUserInfoDetailDTOAsync(mainCommentDTO);

            return mainCommentDTO;
        }

        public async Task<SubCommentDTO> GetSubCommentByIdAsync(int id)
        {
            var subCommentEntity = await _commentRepository.GetSubCommentByIdAsync(id);

            if (subCommentEntity == null) return null;

            var subCommentDTO = _mapper.Map<SubCommentDTO>(subCommentEntity);

            await BindUserInfoDetailDTOAsync(subCommentDTO);

            return subCommentDTO;
        }

        public async Task<List<MainCommentDTO>> GetMainCommentsByBlogIdAsync(int id)
        {
            var mainCommentEntities = await _commentRepository.GetMainCommentsByBlogIdAsync(id);

            var mainCommentDTOs = _mapper.Map<List<MainCommentDTO>>(mainCommentEntities.ToList());

            foreach (var mainCommentDTO in mainCommentDTOs)
            {
                await BindUserInfoDetailDTOAsync(mainCommentDTO);
                await BindUserInfoDetailDTOsAsync(mainCommentDTO.SubCommentDTOs);
            }
            // mainCommentDTOs.ForEach(async s => await BindUserInfoDetailDTOAsync(s));

            return mainCommentDTOs;
        }

        public async Task<List<SubCommentDTO>> GetSubCommentsByBlogIdAsync(int id)
        {
            var subCommentEntities = await _commentRepository.GetSubCommentsByBlogIdAsync(id);

            var subCommentDTOs = _mapper.Map<List<SubCommentDTO>>(subCommentEntities.ToList());

            subCommentDTOs.ForEach(async s => await BindUserInfoDetailDTOAsync(s));

            await BindUserInfoDetailDTOsAsync(subCommentDTOs);

            return subCommentDTOs;
        }

        public async Task<MainCommentDTO> InsertMainCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            Preconditions.NotNull(commentCreationDTO, nameof(commentCreationDTO));

            if (!commentCreationDTO.IsMain)
            {
                throw new InvalidOperationException("Can not insert the SubComment into MainComment Table");
            }

            var mainCommentEntity = _mapper.Map<MainComment>(commentCreationDTO);

            mainCommentEntity.ApplicationUserID = await _currentUserProvider.GetCurrentUserIDAsync();

            var insertedMainComment = await _commentRepository.InsertMainCommentAsync(mainCommentEntity);

            return _mapper.Map<MainCommentDTO>(insertedMainComment);
        }

        public async Task<SubCommentDTO> InsertSubCommentAsync(CommentCreationDTO commentCreationDTO)
        {
            Preconditions.NotNull(commentCreationDTO, nameof(commentCreationDTO));

            if (commentCreationDTO.IsMain)
            {
                throw new InvalidOperationException("Can not insert the MainComment into SubComment Table");
            }

            var subCommentEntity = _mapper.Map<SubComment>(commentCreationDTO);

            subCommentEntity.ApplicationUserID = await _currentUserProvider.GetCurrentUserIDAsync();

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

        private async Task BindUserInfoDetailDTOsAsync(IEnumerable<CommentDTOBase> commentDTOBases)
        {
            foreach (var commentDTOBase in commentDTOBases)
            {
                await BindUserInfoDetailDTOAsync(commentDTOBase);
            }
        }

        private async Task BindUserInfoDetailDTOAsync(CommentDTOBase commentDTOBase)
        {
            commentDTOBase.UserInfoDetailDTO = await _accountService
                .GetUserInfoDetailDTOAsync(commentDTOBase.ApplicationUserID);
        }
    }
}