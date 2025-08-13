namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; } // Unique identifier for the booking
        public int RoomId { get; set; } // Foreign key to the booked room     public DateTime CheckInDate { get; set; } // Check-in date for the booking
        public int UserId { get; set; } // Foreign key to the user who made the booking
        public DateTime CheckInDate { get; set; } // Check-in date for the booking
        public DateTime CheckOutDate { get; set; } // Check-out date for the booking
        public string Status { get; set; } // Status of the booking (e.g., "Confirmed", "Cancelled")
    }
}
