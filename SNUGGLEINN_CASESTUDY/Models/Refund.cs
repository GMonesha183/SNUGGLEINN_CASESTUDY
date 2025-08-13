using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SNUGGLEINN_CASESTUDY.Models
{
    public class Refund
    {
        [Key]
        public int RefundId { get; set; }
        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; } // Foreign key to Booking
        public Booking Booking { get; set; } // Navigation property to Booking

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property to User

        [Required,Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; } // Amount to be refunded
        [Required,MaxLength(500)]
        public string Status { get; set; } // Status of the refund (e.g., "Pending", "Processed", "Rejected")

        public DateTime RequestedAt { get; set; } = DateTime.Now;//estamp of when the refund was requested
        public DateTime? ProcessedAt { get; set; } // Timestamp of when the refund was processed, nullable if not yet processed

    }
}
