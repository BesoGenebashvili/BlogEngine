using BlogEngine.Shared.DTOs;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface INotificationReceiverService : IDataServiceBase<NotificationReceiverDTO, NotificationReceiverCreationDTO, NotificationReceiverCreationDTO>
    {
    }
}