using BlogEngine.Shared.DTOs;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IAccountClient
    {
        Task<UserTokenDTO> LoginAsync(UserLoginDTO userLoginDTO);
        Task<UserTokenDTO> RegisterAsync(UserRegisterDTO userRegisterDTO);
    }
}