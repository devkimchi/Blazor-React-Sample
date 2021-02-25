using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BlazorProxySampleNet
{
    public static class HttpTrigger
    {
        private static HttpClient http = new HttpClient();

        [FunctionName(nameof(HttpTrigger.Run))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "hello")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var baseUri = Environment.GetEnvironmentVariable("API__BASE_URI");
            var endpoint = Environment.GetEnvironmentVariable("API__ENDPOINT");
            var authKey = Environment.GetEnvironmentVariable("API__AUTH_KEY") ?? string.Empty;
            var count = int.TryParse(req.Query["count"], out int result) ? result : 0;
            var requestUri = new Uri($"{baseUri.TrimEnd('/')}/{endpoint.TrimStart('/')}?count={count}&code={authKey}");

            using (var res = await http.GetAsync(requestUri).ConfigureAwait(false))
            {
                dynamic data = await res.Content.ReadAsAsync<object>().ConfigureAwait(false);
                var response = new { text = data.message };

                return new OkObjectResult(response);
            }
        }
    }
}
