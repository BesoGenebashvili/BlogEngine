using BlogEngine.Core.Data.Entities;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface ICustomerReviewRepository : IAsyncRepository<CustomerReview>, IRepository<CustomerReview>
    {
    }
}