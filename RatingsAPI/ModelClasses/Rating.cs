﻿namespace RatingsAPI.ModelClasses
{
    internal class Rating
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string productId { get; set; }
        public DateTime timestamp { get; set; }
        public string locationName { get; set; }
        public int rating { get; set; }
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
    }
}
