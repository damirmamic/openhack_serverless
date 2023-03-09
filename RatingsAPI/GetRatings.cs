using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RatingsAPI.CosmosHandler;
using RatingsAPI.GuardClauses;
using RatingsAPI.ModelClasses;

namespace RatingsAPI
{
    public class GetRatings
    {
        private readonly ILogger _logger;
        private ICosmosDBClientHandler cosmosHandler;
        protected ICosmosDBClientHandler CosmosHandler { get; set; }

        public GetRatings(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetRatings>();
            CosmosHandler = new CosmosDBClientHandler();
        }

        [Function("GetRatings")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, 
        //string userId,
        [CosmosDBInput(databaseName: "ratings",
                       containerName: "ratingContainer",
                       Connection = "CosmosDBConnectionString",
                       SqlQuery = "select * from ratingContainer r where r.userId = {userId}")] 
                       IEnumerable<RatingRead> ratings)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            
            if (ratings == null || !ratings.Any())
            {
                return ResponseCreator.CreateNotFoundResponse(req);
            }

            List<Rating> retVal = new List<Rating>(ratings.Count());

            foreach (var ratingRead in ratings)
            {
                retVal.Add(new Rating(ratingRead));
            }


            return ResponseCreator.CreateOKResponse(req, ratings);
           
        }




    }
}
