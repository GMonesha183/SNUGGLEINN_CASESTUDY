namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class BookingCreateDto
    {
        public int RoomId { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }

        // ✅ Optional additions
        public int Guests { get; set; }       // number of guests
        public int HotelId { get; set; }      // hotel reference (if needed)
    }
}
