using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNUGGLEINN_CASESTUDY.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; } // Foreign key to Hotel
        public Hotel Hotel { get; set; } // Navigation property to Hotel

        [Required, MaxLength(50)]
        public string RoomType { get; set; } // e.g., "Single", "Double", "Suite"
        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerNight { get; set; } // Price per night for the room

        public bool IsAvailable { get; set; } // Availability status of the room

        [MaxLength(200)]
        public string Amenities { get; set; } // Additional amenities provided in the room

        public string ImgUrl { get; set; } // URL for the room image

        public ICollection<Booking> Bookings { get; set; } // Navigation property for bookings associated with the room
    }
}
