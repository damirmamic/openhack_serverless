using RatingsAPI.ModelClasses;

namespace RatingsAPI.CosmosHandler
{
    public interface ICosmosDBClientHandler
    {
        //object StoreRating(Rating rating);

        //Rating GetRatingByAsync(string ratingId);
        Task<Rating?> GetRatingByAsync(string ratingId);

        //IEnumerable<Rating> GetRatingsByAsync(string userId);
        Task<IEnumerable<Rating>?> GetRatingsByAsync(string userId);
    }
}
