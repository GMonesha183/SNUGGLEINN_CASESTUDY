namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class ReviewCreateDto
    {
        public int HotelId { get; set; }
        public int Rating { get; set; } // 1 to 5
        public string Comment { get; set; }
        public IFormFile? File { get; set; }
    }
}
