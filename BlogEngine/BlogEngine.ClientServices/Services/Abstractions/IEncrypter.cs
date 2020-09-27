using BlogEngine.ClientServices.ServiceHelpers;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IEncrypter
    {
        EncrypterOperationResult Encrypt(string text);
        EncrypterOperationResult Decrypt(string text);
    }
}