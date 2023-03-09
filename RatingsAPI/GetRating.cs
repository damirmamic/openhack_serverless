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
        //String ratingId,
        [CosmosDBInput(databaseName: "ratings",
                       containerName: "ratingContainer",
                       Connection = "CosmosDBConnectionString",
                       Id ="{Query.id}")] Rating rating)
        {
            _logger.LogInformation("Get Rating function called.");

            //var rating = CosmosHandler.GetRatingBy(ratingId);

            if (rating == null)
            {
                return ResponseCreator.CreateNotFoundResponse(req);
            }

            return ResponseCreator.CreateOKResponse(req, rating);
        }
    }
}
