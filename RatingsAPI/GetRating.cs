using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RatingsAPI.CosmosHandler;
using RatingsAPI.GuardClauses;
using RatingsAPI.ModelClasses;

namespace RatingsAPI
{
    public class GetRating
    {
        private readonly ILogger _logger;

        private ICosmosHandler cosmosHandler;

        protected ICosmosHandler CosmosHandler { get; set; }

        public GetRating(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetRatings>();
            CosmosHandler = new CosmosDBHandler();
        }

        [Function("GetRating")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,String ratingId)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var rating = CosmosHandler.GetRatingBy(ratingId);

            return ResponseCreator.CreateOKResponse(req, rating);
        }
    }
}
