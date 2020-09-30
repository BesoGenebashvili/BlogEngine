using BlogEngine.Core.Data.Entities;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ICurrentUserProvider
    {
        Task<ApplicationUser> GetCurrentUser();
        Task<int> GetCurrentUserID();
    }
}