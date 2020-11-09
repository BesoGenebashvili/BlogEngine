using System;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions.Utilities;
using BlogEngine.Shared.DTOs.Blog;
using BlogEngine.Shared.Models;

namespace BlogEngine.Server.Services.Implementations.Utilities
{
    public class NotificationSender : INotificationSender
    {
        private readonly IMailService _mailService;
        private readonly INotificationBodyBuilder _notificationBodyBuilder;
        private readonly INotificationReceiverRepository _notificationReceiverRepository;

        public NotificationSender(
            IMailService mailService,
            INotificationBodyBuilder notificationBodyBuilder,
            INotificationReceiverRepository notificationReceiverRepository)
        {
            _mailService = mailService;
            _notificationBodyBuilder = notificationBodyBuilder;
            _notificationReceiverRepository = notificationReceiverRepository;
        }

        public async Task<bool> SendBlogPostNotificationsAsync(BlogDTO blogDTO)
        {
            try
            {
                await ProcessSendBlogPostNotificationsAsync(blogDTO);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task ProcessSendBlogPostNotificationsAsync(BlogDTO blogDTO)
        {
            var notificationReceivers = await _notificationReceiverRepository.GetAllAsync();

            var mailModelsTasks = notificationReceivers.Select(async n =>
            {
                return new MailModel()
                {
                    EmailAddress = n.EmailAddress,
                    DisplayName = n.DisplayName,
                    Body = await _notificationBodyBuilder.BuildBlogPostNotificationBodyAsync(n, blogDTO),
                    IsBodyHtml = true,
                    Subject = "Blog Post Notification"
                };
            });

            var mailModels = await Task.WhenAll(mailModelsTasks);

            mailModels.ToList().ForEach(m => _mailService.SendAsync(m));
        }
    }
}