using BlogEngine.Shared.DTOs.Notification;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface INotificationReceiverService : IDataServiceBase<NotificationReceiverDTO, NotificationReceiverCreationDTO, NotificationReceiverCreationDTO>
    {
    }
}