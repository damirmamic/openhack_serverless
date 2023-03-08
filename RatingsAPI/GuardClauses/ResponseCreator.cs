using Microsoft.Azure.Functions.Worker.Http;
using RatingsAPI.ModelClasses;
using System.Net;

namespace RatingsAPI.GuardClauses
{
    internal class ResponseCreator
    {
        public static HttpResponseData CreateOKResponse<T>(HttpRequestData req, T responseData)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            response.WriteAsJsonAsync<T>(responseData);

            return response;
        }

        public static HttpResponseData CreateInvalidRequestResponse(HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.BadRequest);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Invalid request.");

            return response;
        }

        public static HttpResponseData CreateNotFoundResponse(HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.NotFound);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Request not found.");

            return response;
        }
    }
}
