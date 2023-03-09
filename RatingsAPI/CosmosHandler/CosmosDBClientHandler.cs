
using RatingsAPI.ModelClasses;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace RatingsAPI.CosmosHandler
{
     internal class CosmosDBClientHandler : ICosmosDBClientHandler
    {
      
        //[CosmosDBInput("ratings", "ratingContainer", ConnectionStringSetting = "CosmosDBConnectionString", Id ="{ratingId}",  PartitionKey ="{partitionKey}")]
        public async Task<Rating?> GetRatingByAsync(string ratingId)
        {
            List<Rating> ratings = new List<Rating>();

            try
            {
                // Create a new Cosmos DB client instance
                CosmosClient client = new CosmosClient("AccountEndpoint=https://dbopenhack.documents.azure.com:443/;AccountKey=8mACCpRZd82cWPrrfA3yxej446nRHi2LFrqw9qEPKDoZH49KkVBFM8WaSme4xdAkn9aNZmtacJvzACDbiQ5SZw==;");

                // Get a reference to the container
                Container container = client.GetContainer("ratings", "ratingContainer");

                // Set up a query to retrieve all documents in the container
                QueryDefinition query = new QueryDefinition($"SELECT * FROM c WHERE c.id =\"{ratingId}\"");
                FeedIterator<Rating> iterator = container.GetItemQueryIterator<Rating>(query);

                // Iterate over the results and add them to the list
                while (iterator.HasMoreResults)
                {
                    FeedResponse<Rating> response = await iterator.ReadNextAsync();
                    ratings.AddRange(response);
                }

                return ratings[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting rating with id=\"{ratingId}\": {ex.Message}");

                return null;
            }
        }

        public async Task<IEnumerable<Rating>?> GetRatingsByAsync(string userId)
        {
            List<Rating> ratings = new List<Rating>();

            try
            {
                // Create a new Cosmos DB client instance
                CosmosClient client = new CosmosClient("AccountEndpoint=https://dbopenhack.documents.azure.com:443/;AccountKey=8mACCpRZd82cWPrrfA3yxej446nRHi2LFrqw9qEPKDoZH49KkVBFM8WaSme4xdAkn9aNZmtacJvzACDbiQ5SZw==;");

                // Get a reference to the container
                Container container = client.GetContainer("ratings", "ratingContainer");

                // Set up a query to retrieve all documents in the container
                QueryDefinition query = new QueryDefinition($"SELECT * FROM c WHERE c.userId =\"{userId}\"");
                FeedIterator<Rating> iterator = container.GetItemQueryIterator<Rating>(query);

                // Iterate over the results and add them to the list
                while (iterator.HasMoreResults)
                {
                    FeedResponse<Rating> response = await iterator.ReadNextAsync();
                    ratings.AddRange(response);
                }

                return ratings;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting ratings with  userId=\"{userId}\": {ex.Message}");
                return null;
            }
        }
    }
}