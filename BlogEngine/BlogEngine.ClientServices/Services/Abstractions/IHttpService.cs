using BlogEngine.ClientServices.Common.Models;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Abstractions
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<TResponse>> Get<TResponse>(string url);
        Task<HttpResponseWrapper<object>> Post<TData>(string url, TData data);
        Task<HttpResponseWrapper<TResponse>> Post<TData, TResponse>(string url, TData data);
        Task<HttpResponseWrapper<object>> Put<TData>(string url, TData data);
        Task<HttpResponseWrapper<TResponse>> Put<TData, TResponse>(string url, TData data);
        Task<HttpResponseWrapper<object>> Delete(string url);
        Task<HttpResponseWrapper<TResponse>> Delete<TResponse>(string url);
    }
}