using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureWebShell.Domain.Interfaces.Services 
{
    public interface IAzureLogsService
    {
        Task<IEnumerable<Dictionary<string, string>>> GetAzureAppLogs(string appId, string appKey, string query);
    }
}