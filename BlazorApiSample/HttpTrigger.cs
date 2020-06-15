using System.Threading.Tasks;

using Humanizer;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BlazorApiSample
{
    public static class HttpTrigger
    {
        [FunctionName(nameof(HttpTrigger.Run))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "hello")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var count = req.Query["count"].ToString();
            var ordinalised = default(string);
            try
            {
                ordinalised = string.IsNullOrWhiteSpace(count) ? "0".Ordinalize() : count.Ordinalize();
            }
            catch
            {
                ordinalised = "undefined";
            }

            var message = new { message = $"{ordinalised} world!" };
            var result = new OkObjectResult(message);

            return await Task.FromResult(result).ConfigureAwait(false);
        }
    }
}
