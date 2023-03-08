using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RatingsAPI.CosmosHandler;
using RatingsAPI.GuardClauses;
using RatingsAPI.ModelClasses;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RatingsAPI
{
    public class CreateRating
    {
        private readonly ILogger _logger;

        private ICosmosHandler cosmosHandler;

        protected ICosmosHandler CosmosHandler { get; set; }

        public CreateRating(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CreateRating>();
        }

        [Function("CreateRating")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Create Rating function called.");

            var ratingsRequest = req.ReadFromJsonAsync<RatingsRequest>().Result;

            if (ratingsRequest == null)
            {
                return ResponseCreator.CreateInvalidRequestResponse(req);
            }

            if (ratingsRequest.rating < 0 || ratingsRequest.rating > 5)
            {
                return ResponseCreator.CreateInvalidRequestResponse(req);
            }

            if (!ValidateInputParams<User>(ratingsRequest.userId, "api/GetUser?userId="))
            {
                return ResponseCreator.CreateInvalidRequestResponse(req);
            }

            if (!ValidateInputParams<Product>(ratingsRequest.productId, "api/GetProduct?productId="))
            {
                return ResponseCreator.CreateInvalidRequestResponse(req);
            }

            Rating rating = new Rating(ratingsRequest);

            try
            {
                CosmosHandler.StoreRating(rating);
            }
            catch (Exception)
            {
                return ResponseCreator.CreateInvalidRequestResponse(req);
            }

            return ResponseCreator.CreateOKResponse(req, rating);
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
