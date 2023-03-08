using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RatingsAPI.GuardClauses;
using RatingsAPI.ModelClasses;

namespace RatingsAPI
{
    public class CreateRating
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public CreateRating(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CreateRating>();
            _httpClient = new HttpClient();
        }

        [Function("CreateRating")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Create Rating function called.");


            var ratingsRequest = req.ReadFromJsonAsync<RatingsRequest>().Result;

            if (ratingsRequest == null)
            {
                return Guards.CreateInvalidRequestResponse(req);
            }

            User user;

            if (!TryGetUser(ratingsRequest.userId, out user))
            {
                return Guards.CreateInvalidRequestResponse(req);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }


        private bool TryGetUser(string userId, out User user)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://serverlessohapi.azurewebsites.net/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var userQueryResult = client.GetAsync("api/GetUser?userId=" + userId).Result;
            if (userQueryResult.IsSuccessStatusCode)
            {
                // Parse the response body
                user = userQueryResult.Content.ReadFromJsonAsync<User>().Result;
                return true;
            }
            user = null;
            return false;
        }
    }
}
