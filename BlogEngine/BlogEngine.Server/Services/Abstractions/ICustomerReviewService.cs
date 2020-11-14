using BlogEngine.Shared.DTOs.CustomerReview;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ICustomerReviewService : IDataServiceBase<CustomerReviewDTO, CustomerReviewCreationDTO, CustomerReviewCreationDTO>
    {
    }
}