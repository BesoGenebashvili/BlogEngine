using BlogEngine.ClientServices.ServiceHelpers;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IAsyncEncrypter
    {
        Task<EncrypterOperationResult> EncryptAsync(string text);
        Task<EncrypterOperationResult> DecryptAsync(string text);
    }
}