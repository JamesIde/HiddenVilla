using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {

        //Create a new hotel room DTO, then return that DTO
    public Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);

        //Update hotel
        public Task<HotelRoomDTO> UpdateHotelRoom(int roomId,HotelRoomDTO hotelRoomDTO);

        //Get individiaul hotel
        public Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkIn = null, string checkOut = null);
        public Task<int> DeleteHotelRoom(int roomId);


        //Get all hotels
        public Task<IEnumerable<HotelRoomDTO>> GetAllHotelRoom(string checkIn = null, string checkOut = null);

        //Get the name of the hotel room to validate booking

        public Task<bool> IsBooked(int RoomId, string checkInDate, string checkOutDate);

    }
}
