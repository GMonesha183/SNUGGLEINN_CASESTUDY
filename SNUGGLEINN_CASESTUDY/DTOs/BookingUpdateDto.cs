namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class BookingUpdateDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
        public int Guests { get; set; }          // Updated number of guests
        public decimal TotalPrice { get; set; }  // Updated total price (optional)
    }
}
