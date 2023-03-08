using Microsoft.AspNetCore.Mvc;
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
            CosmosHandler = new CosmosDBHandler();
        }

        [Function("CreateRating")]
       
        public RatingOutput Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Create Rating function called.");

            var ratingsRequest = req.ReadFromJsonAsync<RatingsRequest>().Result;

            HttpResponseData response = null;

            if (ratingsRequest == null)
            {
                response = ResponseCreator.CreateInvalidRequestResponse(req);

                return new RatingOutput()
                {
                    Response = response
                };
            }

            if (ratingsRequest.rating < 0 || ratingsRequest.rating > 5)
            {
                response = ResponseCreator.CreateInvalidRequestResponse(req);

                return new RatingOutput()
                {
                    Response = response
                };
            }

            if (!ValidateInputParams<User>(ratingsRequest.userId, "api/GetUser?userId="))
            {
                response = ResponseCreator.CreateInvalidRequestResponse(req);

                return new RatingOutput()
                {
                    Response = response
                };
            }

            if (!ValidateInputParams<Product>(ratingsRequest.productId, "api/GetProduct?productId="))
            {
                response = ResponseCreator.CreateNotFoundResponse(req);

                return new RatingOutput()
                {
                    Response = response
                };
            }

            Rating rating = new Rating(ratingsRequest);


            response = ResponseCreator.CreateOKResponse(req, rating);
            var retVal = new RatingOutput()
            {
                Rating = rating,
                Response = response
            };
            return retVal;
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
