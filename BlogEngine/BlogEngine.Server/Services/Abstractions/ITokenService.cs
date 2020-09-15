using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions
{
    public interface ITokenService
    {
        Task<UserTokenDTO> BuildToken(UserInfoDTO userInfoDTO);
    }
}