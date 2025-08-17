using System.Globalization;

namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class RoomUpdateDto
    {
        public string RoomType { get; set; } // Type of the room (e.g., "Single", "Double", "Suite")
        public decimal PricePerNight { get; set; } // Price per night for the room
        public bool IsAvailable { get; set; } // Availability status of the room
        public string Amenities { get; set; } // Additional amenities provided in the room
        public string ImgUrl { get; set; } // URL for the room image
    }
}
