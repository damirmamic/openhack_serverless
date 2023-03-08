using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using RatingsAPI.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingsAPI.CosmosHandler
{
    public class RatingOutput
    {
        [CosmosDBOutput("ratings", "ratingContainer", Connection = "CosmosDBConnectionString", CreateIfNotExists = true)]
        public Rating Rating{ get; set; }
        public HttpResponseData Response { get; set; }            
    }
}
