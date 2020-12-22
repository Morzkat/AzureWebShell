using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureWebShell.Domain.Interfaces.Client 
{
    public interface IWebClient 
    {
        Task<T> GetAsync<T>(string url);

        void SetHeaders(Dictionary<string, string> headers);
    }
}