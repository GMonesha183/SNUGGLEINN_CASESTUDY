using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using SNUGGLEINN_CASESTUDY.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        // GET: api/hotel
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();
            var hotelDtos = hotels.Select(h => new HotelDto
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Location = h.Location,
                Description = h.Description,
                ImageUrl = h.ImageUrl,
                OwnerId = h.OwnerId
            }).ToList();

            return Ok(hotelDtos);
        }

        // GET: api/hotel/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (hotel == null) return NotFound();

            var hotelDto = new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Location = hotel.Location,
                Description = hotel.Description,
                ImageUrl = hotel.ImageUrl,
                OwnerId = hotel.OwnerId
            };

            return Ok(hotelDto);
        }

        // POST: api/hotel
        [Authorize(Roles = "Owner,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] HotelCreateDto hotelCreateDto)
        {
            var hotel = new Hotel
            {
                Name = hotelCreateDto.Name,
                Location = hotelCreateDto.Location,
                Description = hotelCreateDto.Description,
                ImageUrl = hotelCreateDto.ImageUrl,
                OwnerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) // set OwnerId from logged-in user
            };

            await _hotelRepository.AddHotelAsync(hotel);
            return Ok(new { message = "Hotel added successfully" });
        }

        // PUT: api/hotel/{id}
        [Authorize(Roles = "Owner,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelUpdateDto hotelUpdateDto)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (hotel == null) return NotFound();

            // Owners can only update their own hotels
            if (User.IsInRole("Owner") && hotel.OwnerId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Forbid();

            hotel.Name = hotelUpdateDto.Name;
            hotel.Location = hotelUpdateDto.Location;
            hotel.Description = hotelUpdateDto.Description;
            hotel.ImageUrl = hotelUpdateDto.ImageUrl;

            await _hotelRepository.UpdateHotelAsync(hotel);
            return Ok(new { message = "Hotel updated successfully" });
        }

        // DELETE: api/hotel/{id}
        [Authorize(Roles = "Owner,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (hotel == null) return NotFound();

            // Owners can only delete their own hotels
            if (User.IsInRole("Owner") && hotel.OwnerId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                return Forbid();

            await _hotelRepository.DeleteHotelAsync(id);
            return Ok(new { message = "Hotel deleted successfully" });
        }
    }
}
