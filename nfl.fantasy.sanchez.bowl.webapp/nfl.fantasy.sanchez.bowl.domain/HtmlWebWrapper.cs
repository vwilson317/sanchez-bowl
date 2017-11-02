using System.Threading.Tasks;
using HtmlAgilityPack;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public class HtmlWebWrapper : IHtmlWebWrapper
    {
        private readonly HtmlWeb _htmlWeb;

        public HtmlWebWrapper()
        {
            _htmlWeb = new HtmlWeb();
        }

        public Task<HtmlDocument> LoadFromWebAsync(string url)
        {
            return _htmlWeb.LoadFromWebAsync(url);
        }
    }
}