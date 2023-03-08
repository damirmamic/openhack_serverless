namespace RatingsAPI.ModelClasses
{
    internal class RatingRequest
    {
        
        public string userId { get; set; }
        public string productId { get; set; }       
        public string locationName { get; set; }
        public int rating { get; set; }
        public string userNotes { get; set; }
    }
}
