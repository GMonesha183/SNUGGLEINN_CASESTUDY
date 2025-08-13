namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class RoomCreateDto
    {
        public int HotelId { get; set; } // Foreign key to the hotel
        public string RoomType { get; set; } // Type of the room (e.g., "Single", "Double", "Suite")
        public decimal PricePerNight { get; set; } // Price per night for the room
        public bool IsAvailable { get; set; } // Availability status of the room
        public string Amenities { get; set; } // Additional amenities provided in the room
    }
}
