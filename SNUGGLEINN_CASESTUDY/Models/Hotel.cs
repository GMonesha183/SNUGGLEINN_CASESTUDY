using System.ComponentModel.DataAnnotations;

namespace SNUGGLEINN_CASESTUDY.Models
{
public class Hotel
    {
        public int HotelId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(200)]
        public string ImageUrl { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; } // Navigation property to the owner of the hotel

        public ICollection<Room> Rooms { get; set; } // Navigation property for rooms in the hotel

        public ICollection<Booking> Bookings { get; set; } // Navigation property for bookings associated with the hotel
        public ICollection<Review> Reviews { get; set; } // Navigation property for reviews associated with the hotel
    }
}