namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class RefundCreateDto
    {
        public int BookingId { get; set; } // Unique identifier for the booking associated with the refund
        public int UserId { get; set; } // Unique identifier for the user requesting the refund
        public decimal Amount { get; set; } // Amount to be refunded
    }
}
