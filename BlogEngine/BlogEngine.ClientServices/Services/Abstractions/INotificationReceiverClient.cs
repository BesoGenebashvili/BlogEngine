using BlogEngine.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface INotificationReceiverClient
    {
        Task<NotificationReceiverDTO> GetAsync(int id);
        Task<List<NotificationReceiverDTO>> GetAllAsync();
        Task<NotificationReceiverDTO> CreateAsync(NotificationReceiverCreationDTO notificationReceiverCreationDTO);
        Task<bool> DeleteAsync(int id);
    }
}