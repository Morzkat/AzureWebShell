using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AzureWebShell.Domain.Interfaces.Client;
using Newtonsoft.Json;

namespace AzureWebShell.Infrastructure.Implementations
{
    public class WebClient : IWebClient
    {
        public HttpClient _httpClient { get; set; }
        public WebClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }

        public void SetHeaders(Dictionary<string, string> headers)
        {
            foreach (var key in headers.Keys)
            {
                _httpClient.DefaultRequestHeaders.Add(key, headers[key]);
            }
        }
    }
}