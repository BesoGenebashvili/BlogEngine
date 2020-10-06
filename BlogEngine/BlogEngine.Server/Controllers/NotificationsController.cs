using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs;
using BlogEngine.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BlogEngine.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
    public class NotificationsController : ControllerBase
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

            if (notificationReceiverDTO == null) return NotFound();

            return notificationReceiverDTO;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NotificationReceiverDTO))]
        public async Task<ActionResult<NotificationReceiverDTO>> Post([FromBody] NotificationReceiverCreationDTO notificationReceiverCreationDTO)
        {
            if (notificationReceiverCreationDTO == null)
            {
                return BadRequest();
            }

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