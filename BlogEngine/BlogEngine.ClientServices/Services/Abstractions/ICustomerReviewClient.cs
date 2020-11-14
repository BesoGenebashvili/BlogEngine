using BlogEngine.Shared.DTOs.CustomerReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface ICustomerReviewClient
    {
        Task<List<CustomerReviewDTO>> GetAllAsync();
        Task<bool> CreateAsync(CustomerReviewCreationDTO customerReviewCreationDTO);
        Task<bool> DeleteAsync(int id);
    }
}