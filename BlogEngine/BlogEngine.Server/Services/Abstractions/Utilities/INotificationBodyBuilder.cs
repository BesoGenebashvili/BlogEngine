using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.DTOs.Blog;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions.Utilities
{
    public interface INotificationBodyBuilder
    {
        Task<string> BuildBlogPostNotificationBodyAsync(NotificationReceiver notificationReceiver, BlogDTO blogDTO);
    }
}