using System.Net.Http;
using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public class HttpWrapper : IHttpWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpWrapper()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            return _httpClient.GetAsync(requestUrl);
        }
    }
}