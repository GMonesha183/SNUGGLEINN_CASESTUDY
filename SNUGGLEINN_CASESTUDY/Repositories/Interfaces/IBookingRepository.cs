using System.Collections.Generic;
using System.Threading.Tasks;
using SNUGGLEINN_CASESTUDY.Models;

namespace SNUGGLEINN_CASESTUDY.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
        Task<Booking> GetBookingByIdAsync(int id);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task CancelBookingAsync(int id);
    }
}
