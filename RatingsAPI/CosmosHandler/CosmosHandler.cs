
using RatingsAPI.ModelClasses;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;

namespace RatingsAPI.CosmosHandler
{
     internal class CosmosHandler : ICosmosHandler
    {
        [CosmosDBOutput("dbopenhack", "ratings", Connection = "AccountEndpoint=https://dbopenhack.documents.azure.com:443/;AccountKey=8mACCpRZd82cWPrrfA3yxej446nRHi2LFrqw9qEPKDoZH49KkVBFM8WaSme4xdAkn9aNZmtacJvzACDbiQ5SZw==", CreateIfNotExists = true)]
        public object StoreRating(Rating rating)
        {
            return rating;
        }


        public Rating GetRatingBy(string ratingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rating> GetRatingsBy(string userId)
        {
            throw new NotImplementedException();
        }
    }
}