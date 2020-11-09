using BlogEngine.Shared.Models;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Abstractions.Utilities
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailModel mailModel);
    }
}