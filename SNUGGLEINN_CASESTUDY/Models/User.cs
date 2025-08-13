using System.ComponentModel.DataAnnotations;

namespace SNUGGLEINN_CASESTUDY.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required,MaxLength(100)]
        public string FullName { get; set; }
        [Required,EmailAddress,MaxLength(100)]
        public string Email { get; set; }
        [Required,MaxLength(100)]
        public string Password { get; set; }
        [Required,MaxLength(15)]
        public string Role { get; set; } // e.g., "Admin", "User", "Guest"
        [Required,MaxLength(15)]
        public string PhoneNumber { get; set; }

        public ICollection<Hotel> Hotels { get; set; } 
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Review> Reviews { get; set; } // Navigation property for reviews written by the user
        public ICollection<Refund> Refunds { get; set; } // Navigation property for refunds requested by the user
    }
}
