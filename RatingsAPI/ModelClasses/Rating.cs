using Newtonsoft.Json;

namespace RatingsAPI.ModelClasses
{
    public class Rating
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("userId")]
        public string userId { get; set; }

        [JsonProperty("productId")]
        public string productId { get; set; }

        [JsonProperty("timestamp")]
        public DateTime timestamp { get; set; }

        [JsonProperty("locationName")]
        public string locationName { get; set; }

        [JsonProperty("rating")]
        public int rating { get; set; }

        [JsonProperty("userNotes")]
        public string userNotes { get; set; }

        public Rating(RatingsRequest ratingsRequest)
        {
            id = Guid.NewGuid().ToString();
            userId = ratingsRequest.userId;
            productId = ratingsRequest.productId;
            timestamp = DateTime.UtcNow;
            locationName = ratingsRequest.locationName;
            rating = ratingsRequest.rating;
            userNotes = ratingsRequest.userNotes;
        }

        public Rating(RatingRead ratingsRead)
        {
            id = ratingsRead.id;
            userId = ratingsRead.userId;
            productId = ratingsRead.productId;
            timestamp = ratingsRead.timestamp;
            locationName = ratingsRead.locationName;
            rating = ratingsRead.rating;
            userNotes = ratingsRead.userNotes;
        }

    }
}
