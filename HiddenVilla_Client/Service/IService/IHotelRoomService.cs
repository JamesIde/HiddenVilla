using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IHotelRoomService
    {

        public Task<IEnumerable<HotelRoomDTO>>  GetHotelRooms(string checkInDate, string checkOutDate);
        public Task<HotelRoomDTO> GetHotelRoomById(int roomId, string checkInDate, string checkOutDate);
    }
}
