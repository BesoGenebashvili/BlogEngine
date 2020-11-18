using AutoMapper;
using BlogEngine.Core.Common.Exceptions;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.DTOs.CustomerReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
{
    public class CustomerReviewService
        : DataServiceBase<CustomerReview, CustomerReviewDTO, CustomerReviewCreationDTO, CustomerReviewCreationDTO>,
          ICustomerReviewService
    {
        private readonly ICurrentUserProvider _currentUserProvider;

        public CustomerReviewService(
            ICustomerReviewRepository customerReviewRepository,
            ICurrentUserProvider currentUserProvider,
            IMapper mapper)
            : base(customerReviewRepository, mapper)
        {
            _currentUserProvider = currentUserProvider;
        }

        public override async Task<List<CustomerReviewDTO>> GetAllAsync()
        {
            await CheckManageAccess();
            return await base.GetAllAsync();
        }

        public override async Task<CustomerReviewDTO> GetByIdAsync(int id)
        {
            await CheckManageAccess();
            return await base.GetByIdAsync(id);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            await CheckManageAccess();
            return await base.DeleteAsync(id);
        }

        public async Task CheckManageAccess()
        {
            if (!(await _currentUserProvider.IsCurrentUserAdmin()))
            {
                throw new UserAccessException("Only Admins can manage customer reviews");
            }
        }
    }
}