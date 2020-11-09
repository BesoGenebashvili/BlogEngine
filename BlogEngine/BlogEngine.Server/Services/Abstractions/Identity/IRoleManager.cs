using BlogEngine.Core.Data.Entities;
using BlogEngine.Server.Common.Models;
using BlogEngine.Shared.DTOs.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions.Identity
{
    public interface IRoleManager
    {
        Task<AccountOperationResult> AssignRoleAsync(UserRoleDTO userRoleDTO);
        Task<AccountOperationResult> RemoveRoleAsync(UserRoleDTO userRoleDTO);
        Task<List<string>> GetUserRolesAsync(int id);
        Task<List<string>> GetUserRolesAsync(ApplicationUser applicationUser);
    }
}