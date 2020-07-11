using BlogEngine.ClientServices.ServiceHelpers;
using BlogEngine.ClientServices.Services.Abstractions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private JsonSerializerOptions DefaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<HttpResponseWrapper<TResponse>> Get<TResponse>(string url)
        {
            var httpResponseMessage = await _httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var deserializedResponse = await Deserialize<TResponse>(httpResponseMessage, DefaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(deserializedResponse, httpResponseMessage, httpResponseMessage.IsSuccessStatusCode);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, httpResponseMessage, httpResponseMessage.IsSuccessStatusCode);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<TData>(string url, TData data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var jsonDataStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(url, jsonDataStringContent);

            return new HttpResponseWrapper<object>(null, httpResponseMessage, httpResponseMessage.IsSuccessStatusCode);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<TData, TResponse>(string url, TData data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var jsonDataStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(url, jsonDataStringContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var deserializedResponse = await Deserialize<TResponse>(httpResponseMessage, DefaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(deserializedResponse, httpResponseMessage, httpResponseMessage.IsSuccessStatusCode);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, httpResponseMessage, httpResponseMessage.IsSuccessStatusCode);
            }
        }

        public async Task<HttpResponseWrapper<object>> Put<TData>(string url, TData data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var jsonDataStringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpResponseMesssage = await _httpClient.PutAsync(url, jsonDataStringContent);
            return new HttpResponseWrapper<object>(default, httpResponseMesssage, httpResponseMesssage.IsSuccessStatusCode);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var httpResponse = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(default, httpResponse, httpResponse.IsSuccessStatusCode);
        }

        private async Task<TResult> Deserialize<TResult>(HttpResponseMessage httpResponseMessage, JsonSerializerOptions jsonSerializerOptions)
        {
            var jsonResponseData = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResult>(jsonResponseData, jsonSerializerOptions);
        }
    }
}