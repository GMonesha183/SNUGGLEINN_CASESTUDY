namespace SNUGGLEINN_CASESTUDY.DTOs
{
    public class RefundDto
    {
        public int RefundId { get; set; } // Unique identifier for the refund
        public int BookingId { get; set; } // Foreign key to the booking associated with the refund
        public int UserId { get; set; } // Foreign key to the user who requested the refund
        public decimal Amount { get; set; } // Amount refunded
        public string Status { get; set; } // Status of the refund (e.g., "Pending", "Completed", "Failed")
        public DateTime RequestedAt { get; set; } = DateTime.Now; // Timestamp of when the refund was requested
        public DateTime? ProcessedAt { get; set; } // Timestamp of when the refund was processed, nullable if not yet processed
    }
}
