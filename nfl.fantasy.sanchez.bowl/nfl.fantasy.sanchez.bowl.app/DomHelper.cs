using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

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

                    var doc = new HtmlDocument();
                    doc.Load(stream);

                    var xpathQuery = "//tbody/tr[not(contains(@class,'bench'))]";
                    var rosterNodes = doc.DocumentNode.SelectNodes(xpathQuery);

                    var nodes = rosterNodes.Nodes();
                    var playerInfoCell = nodes.Select(n => n.SelectNodes("//td[@class='playerNameAndInfo']")).ToList();//.Select(td => td.First().I

                    var links = playerInfoCell.Select(x => x.Nodes());
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
