using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;

namespace BlogEngine.Core.Services.Implementations
{
    public class CustomerReviewRepository : Repository<CustomerReview>, ICustomerReviewRepository
    {
        public CustomerReviewRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}