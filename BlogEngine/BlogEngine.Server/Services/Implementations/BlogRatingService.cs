using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.Blog;
using BlogEngine.Shared.Helpers;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
{
    public class BlogRatingService : IBlogRatingService
    {
        private readonly IBlogRatingRepository _blogRatingRepository;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IMapper _mapper;

        public BlogRatingService(
            IBlogRatingRepository blogRatingRepository,
            ICurrentUserProvider currentUserProvider,
            IMapper mapper)
        {
            _blogRatingRepository = blogRatingRepository;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
        }

        public async Task InsertAsync(BlogRatingDTO blogRatingDTO)
        {
            Preconditions.NotNull(blogRatingDTO, nameof(blogRatingDTO));

            var user = await _currentUserProvider.GetCurrentUserAsync();

            var currentRating = await _blogRatingRepository.GetByBlogIdAndUserIdAsync(blogRatingDTO.BlogID, user.Id);

            if (currentRating is null)
            {
                var blogRatingEntity = _mapper.Map<BlogRating>(blogRatingDTO);
                blogRatingEntity.ApplicationUserID = user.Id;
                blogRatingEntity.CreatedBy = user.FullName;
                blogRatingEntity.LastUpdateBy = user.FullName;
                await _blogRatingRepository.InsertAsync(blogRatingEntity);
            }
            else
            {
                currentRating.Rate = blogRatingDTO.Rate;
                currentRating.LastUpdateBy = user.FullName;
                await _blogRatingRepository.UpdateAsync(currentRating);
            }
        }
    }
}