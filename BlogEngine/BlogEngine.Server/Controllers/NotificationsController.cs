using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Server.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BlogEngine.Shared.DTOs.Notification;
using BlogEngine.Server.Services.Abstractions.Utilities;

namespace BlogEngine.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationsController : BaseController
    {
        private readonly INotificationReceiverService _notificationReceiverService;

        public NotificationsController(INotificationReceiverService notificationReceiverService, IMailService mailService)
        {
            _notificationReceiverService = notificationReceiverService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationReceiverDTO>))]
        public async Task<ActionResult<List<NotificationReceiverDTO>>> Get()
        {
            return await _notificationReceiverService.GetAllAsync();
        }

        [HttpGet("{id:int}", Name = "getNotificationReceiver")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationReceiverDTO))]
        public async Task<ActionResult<NotificationReceiverDTO>> Get(int id)
        {
            var notificationReceiverDTO = await _notificationReceiverService.GetByIdAsync(id);

            if (notificationReceiverDTO is null) return NotFound();

            return notificationReceiverDTO;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NotificationReceiverDTO))]
        public async Task<ActionResult<NotificationReceiverDTO>> Post([FromBody] NotificationReceiverCreationDTO notificationReceiverCreationDTO)
        {
            var notificationReceiverDTO = await _notificationReceiverService.InsertAsync(notificationReceiverCreationDTO);

            return new CreatedAtRouteResult("getNotificationReceiver", new { notificationReceiverDTO.ID }, notificationReceiverDTO);
        }

        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _notificationReceiverService.DeleteAsync(id);
        }
    }
}