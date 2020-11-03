using BlogEngine.ClientServices.Common.Models;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IEncrypter
    {
        EncrypterOperationResult Encrypt(string text);
        EncrypterOperationResult Decrypt(string text);
    }
}