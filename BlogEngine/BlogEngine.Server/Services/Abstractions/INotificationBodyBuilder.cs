using BlogEngine.Core.Data.Entities;
using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface INotificationBodyBuilder
    {
        Task<string> BuildBlogPostNotificationBodyAsync(NotificationReceiver notificationReceiver, BlogDTO blogDTO);
    }
}