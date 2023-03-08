using RatingsAPI.ModelClasses;

namespace RatingsAPI.CosmosHandler
{
    internal interface ICosmosHandler
    {
        void StoreRating(Rating rating);

        Rating GetRatingBy(string ratingId);

        IEnumerable<Rating> GetRatingsBy(string userId);
    }
}
