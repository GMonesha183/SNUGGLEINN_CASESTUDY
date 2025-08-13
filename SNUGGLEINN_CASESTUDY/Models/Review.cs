using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNUGGLEINN_CASESTUDY.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; } // Foreign key to Hotel
        public Hotel Hotel { get; set; } // Navigation property to Hotel

        [ForeignKey(nameof(User))]
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property to User

        [Required,Range(1, 5)]
        public int Rating { get; set; } // Rating given by the user (1 to 5 stars)

        [MaxLength(1000)]
        public int Comment { get; set; } // Review comment

        public DateTime CreatedAt { get; set; } // Timestamp of when the review was created
    }
}
