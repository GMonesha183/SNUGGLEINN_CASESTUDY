namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class HotelDto
    {
        public int HotelId { get; set; } // Unique identifier for the hotel
        public string Name { get; set; } // Name of the hotel
        public string Location { get; set; } // Location of the hotel
        public string Description { get; set; } // Description of the hotel
        public string ImageUrl { get; set; } // URL to the hotel's image
        public int OwnerId { get; set; } // Unique identifier for the owner of the hotel    
    }
}
