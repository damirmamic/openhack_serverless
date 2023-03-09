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
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        //string id,
        [CosmosDBInput(databaseName: "ratings",
                       containerName: "ratingContainer",
                       Connection = "CosmosDBConnectionString",
                       PartitionKey = "{id}",
                       Id ="{id}")] RatingRead ratingRead)
        {
            _logger.LogInformation("Get Rating function called.");

            //var rating = CosmosHandler.GetRatingBy(ratingId);

            if (ratingRead == null)
            {
                return ResponseCreator.CreateNotFoundResponse(req);
            }

            return ResponseCreator.CreateOKResponse(req, new Rating(ratingRead));
        }
    }
}
