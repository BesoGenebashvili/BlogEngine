using System.Net.Http;
using System.Threading.Tasks;

namespace BlogEngine.ClientServices.ServiceHelpers
{
    public class HttpResponseWrapper<TResponse>
    {
        public TResponse Response { get; set; }
        public bool Success { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public HttpResponseWrapper(TResponse response, HttpResponseMessage httpResponseMessage, bool success)
        {
            Response = response;
            Success = success;
            HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string> GetBody() => await HttpResponseMessage.Content.ReadAsStringAsync();
    }
}