namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class ReviewDto
    {
        public int ReviewId { get; set; } // Unique identifier for the review
        public int HotelId { get; set; } // Foreign key to the hotel being reviewed
        public int UserId { get; set; } // Foreign key to the user who wrote the review
        public int Rating { get; set; } // Rating given by the user (1 to 5 stars)
        public string Comment { get; set; } // Review comment
        public DateTime CreatedAt { get; set; } // Timestamp of when the review was created
    }
}
