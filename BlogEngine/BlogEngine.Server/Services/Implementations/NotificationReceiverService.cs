using AutoMapper;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.Notification;

namespace BlogEngine.Server.Services.Implementations
{
    public class NotificationReceiverService :
        DataServiceBase<NotificationReceiver, NotificationReceiverDTO, NotificationReceiverCreationDTO, NotificationReceiverCreationDTO>,
        INotificationReceiverService
    {
        public NotificationReceiverService(
            INotificationReceiverRepository notificationReceiverRepository,
            IMapper mapper)
            : base(notificationReceiverRepository, mapper)
        {
        }
    }
}