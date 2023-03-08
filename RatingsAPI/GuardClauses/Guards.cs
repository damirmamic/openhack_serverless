using Microsoft.Azure.Functions.Worker.Http;
using RatingsAPI.ModelClasses;
using System.Net;

namespace RatingsAPI.GuardClauses
{
    internal class Guards
    {
        public static HttpResponseData CreateOKResponse(HttpRequestData req, Rating requestResponse)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            response.WriteAsJsonAsync<Rating>(requestResponse);

            return response;
        }

        public static HttpResponseData CreateInvalidRequestResponse(HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.BadRequest);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Invalid request.");

            return response;
        }
    }
}
