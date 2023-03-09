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

        private ICosmosDBClientHandler cosmosHandler;

        protected ICosmosDBClientHandler CosmosHandler { get; set; }

        public GetRating(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetRatings>();
            CosmosHandler = new CosmosDBClientHandler();
        }

        [Function("GetRating")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,String ratingId)
        {
            _logger.LogInformation("Get Rating function called.");

            var rating = CosmosHandler.GetRatingBy(ratingId);

            if (rating != null)
            {
                response = ResponseCreator.CreateOKResponse(req, rating);
            }
            else
            {
                response = ResponseCreator.CreateNotFoundResponse(req);
            }

            return new RatingOutput()
                {
                    Rating = rating,
                    Response = response
                };
        }
    }
}
