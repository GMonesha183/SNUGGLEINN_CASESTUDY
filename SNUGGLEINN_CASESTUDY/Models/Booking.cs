using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNUGGLEINN_CASESTUDY.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; } // Foreign key to Room
        public Room Room { get; set; } // Navigation property to Room

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property to User

        [Required]
        public DateTime CheckInDate { get; set; } // Check-in date for the booking
        [Required]
        public DateTime CheckOutDate { get; set; } // Check-out date for the booking

        [Required, MaxLength(100)]
        public string Status { get; set; } // Booking status (e.g., "Confirmed", "Cancelled", "Completed")

        public ICollection<Refund> Refunds { get; set; } // Navigation property for refunds associated with the booking
    }
}
