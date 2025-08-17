namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }      
        public string Comment { get; set; }  
        public DateTime CreatedAt { get; set; }
        public string? FilePath { get; set; }
    }
}
