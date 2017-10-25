using System;
using System.Collections.Generic;
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

        //Important notes: loads the first teams roster size. 
        //as of 10-24-2017 once nfl.com determines the week is over the url changes 
        public async Task<IEnumerable<PlayInfo>> LoadPageAsync(string loginUrl, int rosterSize = 15)
        {
            var playerInfoObjs = new List<PlayInfo>();

            using (httpClient)
            {
                var response = await httpClient.GetAsync(loginUrl);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    var doc = new HtmlDocument();
                    doc.Load(stream);

                    var xpathQuery = "//tbody//tr";

                    var rosterNodes = doc.DocumentNode.SelectNodes(xpathQuery);

                    var playerInfoCells = rosterNodes.ToList();
                    for (var i = 0; i < rosterSize; i++)
                    {
                        var currentPlayInfoCell = playerInfoCells[i];
                        if (!currentPlayInfoCell.InnerText.ToLower().Equals("bench"))
                        {
                            var playerInfo = new PlayInfo
                            {
                                Name = GetNodeInnerText(currentPlayInfoCell, (n) => n.Descendants().FirstOrDefault(d => d.Name == "a")),
                                Position = GetNodeInnerText(currentPlayInfoCell, (n) => n.Descendants().FirstOrDefault(d => d.Name == "em")),
                                Score = GetNodeInnerText(rosterNodes[i], (n) => n.Descendants().LastOrDefault())
                            };

                            playerInfoObjs.Add(playerInfo);
                        }
                    }
                }

                return playerInfoObjs;
            }
        }

        private string GetNodeInnerText(HtmlNode node, Func<HtmlNode, HtmlNode> func)
        {
            var thisNode = func(node);
            var txt = thisNode.InnerText;
            return txt;
        }
    }

    public class PlayInfo
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Score { get; set; }
    }

    public interface IDomHelper
    {
        Task<IEnumerable<PlayInfo>> LoadPageAsync(string loginUrl, int rosterSize = 15);
    }

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

    public interface IHttpWrapper : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string requestUrl);
    }
}
