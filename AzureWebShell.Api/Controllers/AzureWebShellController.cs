using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using AzureWebShell.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureWebShell.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AzureWebShellController
    {
        private readonly IAzureLogsService _azureLogsService;

        public AzureWebShellController(IAzureLogsService azureLogsService)
        {
            _azureLogsService = azureLogsService;
        }

        [HttpGet("apps/{appId}")]
        public async Task<IEnumerable<Dictionary<string, string>>> GetAzureAppLogs(string appId, string appKey, string query)
        {
            try
            {
                return await _azureLogsService.GetAzureAppLogs(appId, appKey, query);
            }
            catch (System.Exception)
            {

                dynamic response = new ExpandoObject();
                response.Error = "Error processing azure logs, verify the query, AppKey and AppId then try again";

                return response;
            }
        }
    }
}