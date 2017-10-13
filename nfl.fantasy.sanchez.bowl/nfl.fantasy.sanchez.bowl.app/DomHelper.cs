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

        public async Task LoadPageAsync(string loginUrl){
            using(httpClient){
                var response = await httpClient.GetAsync(loginUrl);
                if(response.StatusCode == HttpStatusCode.OK){
                    var stream = await response.Content.ReadAsStreamAsync();

                    var doc = new HtmlDocument();
                    doc.Load(stream);

                    var xpathQuery = "//tbody//tr";
                    var rosterNodes = doc.DocumentNode.SelectNodes(xpathQuery);

                    var palyerInfoXpathQuery = "//td[@class='playerNameAndInfo']";
                    var positionXpathQuery = "//em";
                    var nameXpathQuery = "//a[1]";
                    var scoreXpathQuery = "//td[@class='stat statTotal numeric last']/span";
                    var playerInfoCells = rosterNodes.Select(n => n.SelectSingleNode(palyerInfoXpathQuery)).ToList();//.Select(td => td.First().I

                    var names = playerInfoCells.Select(x => new PlayInfo
                    {
                        Name = GetNodeInnerText(x, nameXpathQuery),
                        Position = GetNodeInnerText(x, positionXpathQuery),
                        Score = GetNodeInnerText(x, scoreXpathQuery)
                    }).ToList();
                }
            }
        }

        private string GetNodeInnerText(HtmlNode node, string xpathQuery){
            var thisNode = node.SelectSingleNode(xpathQuery);
            var txt = thisNode.InnerText;
            return txt;
        }
    }

    public class PlayInfo{
        public string Name { get; set; }
        public string Position { get; set; }
        public string Score { get; set; }
    }

    public interface IDomHelper
    {
        Task LoadPageAsync(string loginUrl);
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
