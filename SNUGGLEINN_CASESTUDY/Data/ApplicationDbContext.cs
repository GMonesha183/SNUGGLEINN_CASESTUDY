using Microsoft.EntityFrameworkCore;
using SNUGGLEINN_CASESTUDY.Models;

namespace SNUGGLEINN_CASESTUDY.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for all models
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER → HOTEL
            modelBuilder.Entity<User>()
                .HasMany(u => u.Hotels)
                .WithOne(h => h.Owner)
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // HOTEL → ROOM
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // ROOM → BOOKING
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // USER → BOOKING (Restrict)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // USER → REVIEW (Restrict)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // HOTEL → REVIEW (Restrict to avoid cascade path conflict)
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Reviews)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

            // USER → REFUND (Restrict)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Refunds)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // BOOKING → REFUND
            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Refunds)
                .WithOne(r => r.Booking)
                .HasForeignKey(r => r.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
