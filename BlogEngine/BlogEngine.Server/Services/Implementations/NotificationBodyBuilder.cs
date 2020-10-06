using System.Text;
using System.Threading.Tasks;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.Services.Implementations
{
    public class NotificationBodyBuilder : INotificationBodyBuilder
    {
        public Task<string> BuildBlogPostNotificationBodyAsync(NotificationReceiver notificationReceiver, BlogDTO blogDTO)
        {
            // TODO : use template.txt for body building
            #region Temporary Code

            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"Hello {notificationReceiver.DisplayName} <br>");
            stringBuilder.Append($"{blogDTO.CreatedBy} just posted new article: <a href='https://localhost:44388/blog/view/{blogDTO.ID}/{blogDTO.Title}'>{blogDTO.Title}</a> <br>");
            stringBuilder.Append($"<a href='https://localhost:44388/notificationReceiver/delete/{notificationReceiver.ID}'> Remove notifications <a>");

            return Task.FromResult(stringBuilder.ToString());
            #endregion
        }
    }
}