using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.ClientServices.Extensions;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class NotificationReceiverClient : INotificationReceiverClient
    {
        private IHttpService _httpService;
        private const string Url = "api/Notifications";

        public NotificationReceiverClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<NotificationReceiverDTO> GetAsync(int id)
        {
            return await _httpService.GetHelperAsync<NotificationReceiverDTO>($"{Url}/{id}");
        }

        public async Task<List<NotificationReceiverDTO>> GetAllAsync()
        {
            return await _httpService.GetHelperAsync<List<NotificationReceiverDTO>>(Url);
        }

        public async Task<NotificationReceiverDTO> CreateAsync(NotificationReceiverCreationDTO notificationReceiverCreationDTO)
        {
            return await _httpService.PostHelperAsync<NotificationReceiverCreationDTO, NotificationReceiverDTO>(Url, notificationReceiverCreationDTO);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{Url}/{id}");
        }
    }
}