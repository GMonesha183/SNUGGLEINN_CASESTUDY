using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using SNUGGLEINN_CASESTUDY.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SNUGGLEINN_CASESTUDY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;

        public RoomController(IRoomRepository roomRepository, IHotelRepository hotelRepository)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
        }

        // GET: api/room/hotel/{hotelId}
        [AllowAnonymous]
        [HttpGet("hotel/{hotelId}")]
        public async Task<IActionResult> GetRoomsByHotelId(int hotelId)
        {
            var rooms = await _roomRepository.GetRoomsByHotelIdAsync(hotelId);

            var roomDtos = rooms.Select(r => new RoomDto
            {
                RoomId = r.RoomId,
                HotelId = r.HotelId,
                RoomType = r.RoomType,
                PricePerNight = r.PricePerNight,
                IsAvailable = r.IsAvailable,
                Amenities = r.Amenities,
                ImgUrl = r.ImgUrl
            }).ToList();

            return Ok(roomDtos);
        }

        // GET: api/room/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            var roomDto = new RoomDto
            {
                RoomId = room.RoomId,
                HotelId = room.HotelId,
                RoomType = room.RoomType,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                Amenities = room.Amenities,
                ImgUrl = room.ImgUrl
            };

            return Ok(roomDto);
        }

        // POST: api/room
        [Authorize(Roles = "Owner,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] RoomCreateDto roomDto)
        {
            if (User.IsInRole("Owner"))
            {
                var hotel = await _hotelRepository.GetHotelByIdAsync(roomDto.HotelId);
                if (hotel == null) return NotFound("Hotel not found");
                if (hotel.OwnerId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                    return Forbid();
            }

            var room = new Room
            {
                HotelId = roomDto.HotelId,
                RoomType = roomDto.RoomType,
                PricePerNight = roomDto.PricePerNight,
                IsAvailable = roomDto.IsAvailable,
                Amenities = roomDto.Amenities,
                ImgUrl = roomDto.ImgUrl
            };

            await _roomRepository.AddRoomAsync(room);
            return Ok(new { message = "Room added successfully" });
        }

        // PUT: api/room/{id}
        [Authorize(Roles = "Owner,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateDto roomDto)
        {
            var existingRoom = await _roomRepository.GetRoomByIdAsync(id);
            if (existingRoom == null) return NotFound("Room not found");

            if (User.IsInRole("Owner"))
            {
                var hotel = await _hotelRepository.GetHotelByIdAsync(existingRoom.HotelId);
                if (hotel == null) return NotFound("Hotel not found");
                if (hotel.OwnerId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                    return Forbid();
            }

            existingRoom.RoomType = roomDto.RoomType;
            existingRoom.PricePerNight = roomDto.PricePerNight;
            existingRoom.IsAvailable = roomDto.IsAvailable;
            existingRoom.Amenities = roomDto.Amenities;
            if (!string.IsNullOrEmpty(roomDto.ImgUrl))
                existingRoom.ImgUrl = roomDto.ImgUrl; // update img if provided

            await _roomRepository.UpdateRoomAsync(existingRoom);
            return Ok(new { message = "Room updated successfully" });
        }

        // DELETE: api/room/{id}
        [Authorize(Roles = "Owner,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _roomRepository.GetRoomByIdAsync(id);
            if (room == null) return NotFound();

            if (User.IsInRole("Owner"))
            {
                var hotel = await _hotelRepository.GetHotelByIdAsync(room.HotelId);
                if (hotel == null) return NotFound("Hotel not found");
                if (hotel.OwnerId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                    return Forbid();
            }

            await _roomRepository.DeleteRoomAsync(id);
            return Ok(new { message = "Room deleted successfully" });
        }
    }
}
