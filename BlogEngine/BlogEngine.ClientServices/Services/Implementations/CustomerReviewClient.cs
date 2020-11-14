using System.Threading.Tasks;
using System.Collections.Generic;
using BlogEngine.ClientServices.Services.Abstractions;
using BlogEngine.Shared.DTOs.CustomerReview;
using BlogEngine.ClientServices.Common.Extensions;
using BlogEngine.ClientServices.Common.Endpoints;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class CustomerReviewClient : ICustomerReviewClient
    {
        private readonly IHttpService _httpService;

        public CustomerReviewClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<CustomerReviewDTO>> GetAllAsync()
        {
            return await _httpService.GetHelperAsync<List<CustomerReviewDTO>>(CustomerReviewClientEndpoints.Base);
        }

        public async Task<bool> CreateAsync(CustomerReviewCreationDTO customerReviewCreationDTO)
        {
            return await _httpService.PostHelperAsync<CustomerReviewCreationDTO, bool>(CustomerReviewClientEndpoints.Base, customerReviewCreationDTO);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _httpService.DeleteHelperAsync<bool>($"{CustomerReviewClientEndpoints.Base}/{id}");
        }
    }
}