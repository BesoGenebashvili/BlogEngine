using BlogEngine.Shared.DTOs.Blog;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface INotificationSender
    {
        Task<bool> SendBlogPostNotificationsAsync(BlogDTO blogDTO);
    }
}