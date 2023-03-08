using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Google.Protobuf.WellKnownTypes;
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

            if (!ValidateInputParams<User>(ratingsRequest.userId, "api/GetUser?userId="))
            {
                return Guards.CreateInvalidRequestResponse(req);
            }

            if (!ValidateInputParams<Product>(ratingsRequest.productId, "api/GetProduct?productId="))
            {
                return Guards.CreateInvalidRequestResponse(req);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"User:{ratingsRequest.userId}");

            return response;
        }


        private bool ValidateInputParams<T>(string id, string param) where T : class
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://serverlessohapi.azurewebsites.net/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var queryResult = client.GetAsync(param + id).Result;
            T queryValue = null;
            if (queryResult.IsSuccessStatusCode)
            {
                // Parse the response body
                queryValue = queryResult.Content.ReadFromJsonAsync<T>().Result;

              
            }
            return queryValue != null;
        }
    }
}
