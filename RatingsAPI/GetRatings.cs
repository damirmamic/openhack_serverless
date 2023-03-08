using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RatingsAPI.ModelClasses;

namespace RatingsAPI
{
    public class GetRatings
    {
        private readonly ILogger _logger;

        public GetRatings(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetRatings>();
        }

        [Function("GetRatings")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, string userId)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");


            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://serverlessohapi.azurewebsites.net/");
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response
            var userQueryResult = client.GetAsync("api/GetUser?userId="+userId).Result;
            if (userQueryResult.IsSuccessStatusCode)
            {
                // Parse the response body
                var dataObjects = userQueryResult.Content.ReadFromJsonAsync<User>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)userQueryResult.StatusCode,
                              userQueryResult.ReasonPhrase);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
