using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNUGGLEINN_CASESTUDY.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey(nameof(Hotel))]
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required, Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        // Soft deletion flag
        public bool IsDeleted { get; set; } = false;

        // File upload path
        [MaxLength(500)]
        public string FilePath { get; set; }
    }
}
