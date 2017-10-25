using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace nfl.fantasy.sanchez.bowl.app
{
    public interface IHttpWrapper : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string requestUrl);
    }
}