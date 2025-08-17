using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNUGGLEINN_CASESTUDY.Data;
using SNUGGLEINN_CASESTUDY.DTOs;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ApplicationDbContext _context;

        public BookingController(IBookingRepository bookingRepository, ApplicationDbContext context)
        {
            _bookingRepository = bookingRepository;
            _context = context;
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (role == "Admin")
            {
                var allBookings = await _bookingRepository.GetAllBookingsAsync();
                return Ok(allBookings.Select(ToDto));
            }

            if (role == "Owner")
            {
                var ownerBookings = await _bookingRepository.GetBookingsByOwnerIdAsync(userId);
                return Ok(ownerBookings.Select(ToDto));
            }

            return Forbid();
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("user")]
        public async Task<IActionResult> GetBookingsForCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
            return Ok(bookings.Select(ToDto));
        }

        [Authorize(Roles = "Guest,Owner,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(ToDto(booking));
        }

        [Authorize(Roles = "Guest")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateDto bookingDto)
        {
            if (bookingDto == null)
                return BadRequest("Invalid booking data.");

            if (!DateTime.TryParseExact(
                    bookingDto.CheckInDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime checkIn) ||
                !DateTime.TryParseExact(
                    bookingDto.CheckOutDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime checkOut))
            {
                return BadRequest("Invalid date format. Use yyyy-MM-dd.");
            }

            if (checkOut <= checkIn)
                return BadRequest("Check-out date must be after check-in date.");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var existingBookings = await _bookingRepository.GetBookingsByRoomIdAsync(bookingDto.RoomId);
            bool hasConflict = existingBookings.Any(b =>
                (checkIn < b.CheckOutDate && checkOut > b.CheckInDate) && b.Status != "Cancelled");

            if (hasConflict)
                return Conflict("Room is already booked for the selected dates.");

            var room = await _context.Rooms.FindAsync(bookingDto.RoomId);
            if (room == null) return BadRequest("Invalid room.");

            var booking = new Booking
            {
                RoomId = bookingDto.RoomId,
                UserId = userId,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                Status = "Pending",
                Guests = bookingDto.Guests,
                TotalPrice = room.PricePerNight * bookingDto.Guests * (decimal)(checkOut - checkIn).TotalDays
            };

            await _bookingRepository.AddBookingAsync(booking);

            return Ok(new { message = "Booking created successfully", bookingId = booking.BookingId });
        }


        [Authorize(Roles = "Guest,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateDto bookingDto)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            if (bookingDto.CheckOutDate <= bookingDto.CheckInDate)
                return BadRequest("Check-out date must be after check-in date.");

            booking.CheckInDate = bookingDto.CheckInDate;
            booking.CheckOutDate = bookingDto.CheckOutDate;
            booking.Status = bookingDto.Status;
            booking.Guests = bookingDto.Guests;

            await _bookingRepository.UpdateBookingAsync(booking);
            return Ok(new { message = "Booking updated successfully" });
        }

        [Authorize(Roles = "Guest,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();

            await _bookingRepository.CancelBookingAsync(id);
            return Ok(new { message = "Booking cancelled successfully" });
        }

        private BookingDto ToDto(Booking b) => new BookingDto
        {
            BookingId = b.BookingId,
            RoomId = b.RoomId,
            UserId = b.UserId,
            CheckInDate = b.CheckInDate,
            CheckOutDate = b.CheckOutDate,
            Status = b.Status,
            HotelName = b.Room?.Hotel?.Name,
            Guests = b.Guests,
            TotalPrice = b.TotalPrice
        };
    }
}
