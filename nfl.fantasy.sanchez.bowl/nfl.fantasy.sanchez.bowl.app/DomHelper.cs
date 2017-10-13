using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
namespace nfl.fantasy.sanchez.bowl.app
{
    public class DomHelper : IDomHelper
    {
        readonly IHttpWrapper httpClient;

        public DomHelper(IHttpWrapper httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task LoadPage(string loginUrl){
            using(httpClient){
                var response = await httpClient.GetAsync(loginUrl);
                if(response.StatusCode == HttpStatusCode.OK){
                    var stream = await response.Content.ReadAsStreamAsync();
                    using (var streamReader = new StreamReader(stream)){
                        var dom = await streamReader.ReadToEndAsync();
                    }
                }
            }
        }
    }

    public interface IDomHelper
    {
        Task LoadPage(string loginUrl);
    }

    public class HttpWrapper:IHttpWrapper{
        private readonly HttpClient _httpClient;

        public HttpWrapper(){
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

    public interface IHttpWrapper: IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string requestUrl);
    }
}
