using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface INotificationBodyBuilder
    {
        Task<string> BuildBlogPostNotificationBodyAsync(BlogDTO blogDTO);
    }
}