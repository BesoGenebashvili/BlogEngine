using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.CustomerReview;
using BlogEngine.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
    public class CustomerReviewsController : BaseController
    {
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewsController(ICustomerReviewService customerReviewService)
        {
            _customerReviewService = customerReviewService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CustomerReviewDTO>))]
        public async Task<ActionResult<List<CustomerReviewDTO>>> Get()
        {
            return await _customerReviewService.GetAllAsync();
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Post([FromBody] CustomerReviewCreationDTO customerReviewCreationDTO)
        {
            return await _customerReviewService.InsertAsync(customerReviewCreationDTO) != null;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            return await _customerReviewService.DeleteAsync(id);
        }
    }
}