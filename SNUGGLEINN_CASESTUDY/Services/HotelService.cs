using SNUGGLEINN_CASESTUDY.DTOs;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Services
{
    public class HotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();
            return hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Description = h.Description,
                ImageUrl = h.ImageUrl,
            });
        }

        public Task<Hotel> GetHotelByIdAsync(int id)
        {
            return _hotelRepository.GetHotelByIdAsync(id);
        }

        public Task AddHotelAsync(Hotel hotel)
        {
            return _hotelRepository.AddHotelAsync(hotel);
        }

        public Task UpdateHotelAsync(Hotel hotel)
        {
            return _hotelRepository.UpdateHotelAsync(hotel);
        }

        public Task DeleteHotelAsync(int id)
        {
            return _hotelRepository.DeleteHotelAsync(id);
        }
    }

}
