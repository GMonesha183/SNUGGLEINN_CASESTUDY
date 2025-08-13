using System.Collections.Generic;
using System.Threading.Tasks;
using SNUGGLEINN_CASESTUDY.Models;

namespace SNUGGLEINN_CASESTUDY.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task AddHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(int id);
    }
}
