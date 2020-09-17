using BlogEngine.ClientServices.Services.Abstractions;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class BrowserStorageService : IBrowserStorageService
    {
        private readonly IJSRuntime _jSRuntime;

        private const string Get = "localStorage.getItem";
        private const string Set = "localStorage.setItem";
        private const string Remove = "localStorage.removeItem";

        public BrowserStorageService(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async ValueTask<string> GetFromLocalStorage(string key)
        {
            return await _jSRuntime.InvokeAsync<string>(Get, key);
        }

        public async ValueTask<object> SetInLocalStorage(string key, string content)
        {
            return await _jSRuntime.InvokeAsync<object>(Set, key, content);
        }

        public async ValueTask<object> RemoveFromLocalStorage(string key)
        {
            return await _jSRuntime.InvokeAsync<object>(Remove, key);
        }
    }
}