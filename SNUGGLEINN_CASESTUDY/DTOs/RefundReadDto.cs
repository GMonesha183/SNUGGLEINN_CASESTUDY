namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class RefundReadDto:RefundDto
    {
        public string UserName { get; set; } // Name of the user requesting the refund
        public string BookingStatus { get; set; } // Status of the booking associated with the refund
    }
}
