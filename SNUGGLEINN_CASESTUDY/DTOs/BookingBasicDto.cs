namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class BookingBasicDto
    {
        public int BookingId { get; set; } // Unique identifier for the booking
        public DateTime CheckInDate { get; set; } // Check-in date for the booking
        public DateTime CheckOutDate { get; set; } // Check-out date for the booking
    }
}
