namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class RoomBasicDto
    {
        public int RoomId { get; set; } // Unique identifier for the room
        public string RoomType { get; set; } // Type of the room (e.g., "Single", "Double", "Suite")
        public decimal PricePerNight { get; set; } // Price per night for the room
    }
}
