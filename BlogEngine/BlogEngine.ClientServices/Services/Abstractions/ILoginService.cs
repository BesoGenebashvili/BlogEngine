using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface ILoginService
    {
        Task Login(string token);
        Task Logout();
    }
}