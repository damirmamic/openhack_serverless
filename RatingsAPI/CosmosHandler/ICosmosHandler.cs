using RatingsAPI.ModelClasses;

namespace RatingsAPI.CosmosHandler
{
    public interface ICosmosHandler
    {
        object StoreRating(Rating rating);

        Rating GetRatingBy(string ratingId);

        IEnumerable<Rating> GetRatingsBy(string userId);
    }
}
