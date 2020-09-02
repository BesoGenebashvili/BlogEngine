using System.Threading.Tasks;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.Services.Implementations
{
    public class NotificationBodyBuilder : INotificationBodyBuilder
    {
        public Task<string> BuildBlogPostNotificationBodyAsync(BlogDTO blogDTO)
        {
            // TODO : use template txt for body building

            return Task.FromResult($"{blogDTO.CreatedBy} just posted new article: {blogDTO.Title}");
        }
    }
}