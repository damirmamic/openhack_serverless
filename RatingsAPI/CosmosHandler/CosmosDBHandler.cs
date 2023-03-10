
using RatingsAPI.ModelClasses;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System.Net.Http.Headers;

namespace RatingsAPI.CosmosHandler
{
     internal class CosmosDBHandler : ICosmosHandler
    {
        [CosmosDBOutput("ratings", "ratingContainer", Connection = "CosmosDBConnectionString", CreateIfNotExists = true)]
        public object StoreRating(Rating rating)
        {
            return rating;
        }
        
        public Rating GetRatingBy(string ratingId)
        {
            RatingsRequest rr = new RatingsRequest()
            {
                locationName = "new location name",
                productId = "new Product ID",
                rating = 3,
                userId = "someUserId",
                userNotes = "new user notes"
            };

            Rating rating = new Rating(rr);
            rating.id = ratingId;
            return rating;
        }

        public IEnumerable<Rating> GetRatingsBy(string userId)
        {
            List<Rating> retVal = new List<Rating>();

            RatingsRequest rr = new RatingsRequest()
            {
                locationName = "new location name",
                productId = "new Product ID",
                rating = 3,
                userId = userId,
                userNotes = "new user notes"
            };

            retVal.Add(new Rating(rr));

            return retVal;
        }
    }
}