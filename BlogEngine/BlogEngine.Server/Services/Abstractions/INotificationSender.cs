using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface INotificationSender
    {
        Task<bool> SendBlogPostNotificationsAsync(BlogDTO blogDTO);
    }
}