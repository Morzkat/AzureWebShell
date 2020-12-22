using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AzureWebShell.Domain.Interfaces.Client;
using AzureWebShell.Domain.Interfaces.Services;

namespace AzureWebShell.Application.Services
{
    public class AzureLogsService : IAzureLogsService
    {
        IWebClient _webClient;
        public AzureLogsService(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public async Task<IEnumerable<Dictionary<string, string>>> GetAzureAppLogs(string appId, string appKey, string query)
        {
            //TODO: Move request to api to another method...
            var headers = new Dictionary<string, string>();
            headers.Add("x-api-key", appKey);

            _webClient.SetHeaders(headers);
            var request_result = await _webClient.GetAsync<dynamic>($"https://api.applicationinsights.io/v1/apps/{appId}/query?query={query}");
            //

            var columns = await GetTableColumnsAsync(request_result["tables"][0]["columns"]);

            return await BuildAzureLogsJson(columns, request_result["tables"][0]["rows"]);
        }

        public Task<List<string>> GetTableColumnsAsync(dynamic columns)
        {
            var result = new List<string>();

            foreach (var column in columns)
                result.Add((string)column["name"]);

            return Task.FromResult(result);
        }

        public async Task<IEnumerable<Dictionary<string, string>>> BuildAzureLogsJson(dynamic columns, dynamic rows)
        {
            List<Task<Dictionary<string, string>>> createLogTasks = new List<Task<Dictionary<string, string>>>();

            foreach (var row in rows)
                createLogTasks.Add(CreateLogElementAsync(columns, row));

            return await Task.WhenAll(createLogTasks);
        }

        public Task<Dictionary<string, string>> CreateLogElementAsync(IList<string> columns, dynamic row)
        {
            dynamic element = new Dictionary<string, string>();

            for (int i = 0; i < Enumerable.Count(row); i++)
                element[columns[i]] = (string)row[i];

            return Task.FromResult(element);
        }

    }
}
