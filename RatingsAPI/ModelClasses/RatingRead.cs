using Newtonsoft.Json;

namespace RatingsAPI.ModelClasses
{
    public class RatingRead
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

        [JsonProperty("_rid")]
        public string _rid { get; set; }

        [JsonProperty("_self")]
        public string _self { get; set; }

        [JsonProperty("_etag")]
        public string _etag { get; set; }

        [JsonProperty("_attachments")]
        public string _attachments { get; set; }

        [JsonProperty("_ts")]
        public long _ts { get; set; }


    }
}
