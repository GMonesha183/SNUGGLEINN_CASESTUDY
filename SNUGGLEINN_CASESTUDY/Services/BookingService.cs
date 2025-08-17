using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return _bookingRepository.GetBookingsByUserIdAsync(userId);
        }

        public Task<Booking> GetBookingByIdAsync(int id)
        {
            return _bookingRepository.GetBookingByIdAsync(id);
        }

        public Task AddBookingAsync(Booking booking)
        {
            return _bookingRepository.AddBookingAsync(booking);
        }

        public Task UpdateBookingAsync(Booking booking)
        {
            return _bookingRepository.UpdateBookingAsync(booking);
        }

        public Task CancelBookingAsync(int id)
        {
            return _bookingRepository.CancelBookingAsync(id);
        }
    }
}
