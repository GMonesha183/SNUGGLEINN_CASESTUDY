using SNUGGLEINN_CASESTUDY.Interfaces;
using SNUGGLEINN_CASESTUDY.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SNUGGLEINN_CASESTUDY.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            return _roomRepository.GetRoomsByHotelIdAsync(hotelId);
        }

        public Task<Room> GetRoomByIdAsync(int id)
        {
            return _roomRepository.GetRoomByIdAsync(id);
        }

        public Task AddRoomAsync(Room room)
        {
            return _roomRepository.AddRoomAsync(room);
        }

        public Task UpdateRoomAsync(Room room)
        {
            return _roomRepository.UpdateRoomAsync(room);
        }

        public Task DeleteRoomAsync(int id)
        {
            return _roomRepository.DeleteRoomAsync(id);
        }
    }
}
