using System.Threading.Tasks;
using HtmlAgilityPack;

namespace nfl.fantasy.sanchez.bowl.domain
{
    public interface IHtmlWebWrapper
    {
        Task<HtmlDocument> LoadFromWebAsync(string url);
    }
}