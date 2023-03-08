
using RatingsAPI.ModelClasses;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;

namespace RatingsAPI
{
     public class CosmosDBConnection
    {
        [CosmosDBOutput("dbopenhack", "ratings", 
         Connection = "AccountEndpoint=https://dbopenhack.documents.azure.com:443/;AccountKey=8mACCpRZd82cWPrrfA3yxej446nRHi2LFrqw9qEPKDoZH49KkVBFM8WaSme4xdAkn9aNZmtacJvzACDbiQ5SZw==", CreateIfNotExists = true)]
        public Rating Rating { get; set; }
        public HttpResponseData HttpResponse { get; set; }
    }
}