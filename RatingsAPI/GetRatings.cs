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
        private ICosmosHandler cosmosHandler;
        protected ICosmosHandler CosmosHandler { get; set; }

        public GetRatings(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetRatings>();
            CosmosHandler = new CosmosDBHandler();
        }

        [Function("GetRatings")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, string userId)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            
            var ratings= CosmosHandler.GetRatingsBy(userId);

            if (ratings == null || !ratings.Any())
            {
                return ResponseCreator.CreateNotFoundResponse(req);
            }

            return ResponseCreator.CreateOKResponse(req, ratings);
           
        }




    }
}
