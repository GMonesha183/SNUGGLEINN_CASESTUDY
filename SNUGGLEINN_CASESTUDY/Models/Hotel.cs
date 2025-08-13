using System.ComponentModel.DataAnnotations;

namespace SNUGGLEINN_CASESTUDY.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Location { get; set; }
        [MaxLength(15)]
        public string Description { get; set; }
        public string ImageUrl { get; set; } // URL to the hotel's image

        public int OwnerId { get; set; } // Foreign key to User
        public User Owner { get; set; } // Navigation property

        public ICollection<Room> Rooms { get; set; } // Navigation property for rooms in the hotel

        public ICollection<Booking> Bookings { get; set; } // Navigation property for bookings associated with the hotel
        public ICollection<Review> Reviews { get; set; } // Navigation property for reviews associated with the hotel
    }
}