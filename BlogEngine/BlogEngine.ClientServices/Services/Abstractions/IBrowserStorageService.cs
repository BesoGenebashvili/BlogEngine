using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IBrowserStorageService
    {
        ValueTask<string> GetFromLocalStorage(string key);
        ValueTask<object> SetInLocalStorage(string key, string content);
        ValueTask<object> RemoveFromLocalStorage(string key);
    }
}