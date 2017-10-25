using System.Threading.Tasks;
using HtmlAgilityPack;

namespace nfl.fantasy.sanchez.bowl.app
{
    public interface IHtmlWebWrapper
    {
        Task<HtmlDocument> LoadFromWebAsync(string url);
    }
}