using BlogEngine.Core.Data.Entities;
using System.Threading.Tasks;

namespace BlogEngine.Core.Services.Abstractions
{
    public interface ICurrentUserProvider
    {
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<int> GetCurrentUserIDAsync() => Task.FromResult(GetCurrentUserAsync().GetAwaiter().GetResult().Id);
        Task<bool> IsCurrentUserAdmin();
    }
}