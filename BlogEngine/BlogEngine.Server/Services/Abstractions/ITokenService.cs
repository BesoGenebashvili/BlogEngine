using BlogEngine.Shared.DTOs.Identity;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ITokenService
    {
        Task<UserTokenDTO> BuildToken(UserInfoDTO userInfoDTO);
    }
}