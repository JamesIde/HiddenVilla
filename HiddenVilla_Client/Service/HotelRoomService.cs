using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class HotelRoomService : IHotelRoomService
    {


        //use http client and json helpers like 


        private readonly HttpClient _client;

        public HotelRoomService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate)
        {
                 
            var response = await _client.GetAsync($"api/hotelroom?checkIn={checkInDate}&checkOut={checkInDate}");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDTO>>(content);
            return rooms;
        }
        public async Task<HotelRoomDTO> GetHotelRoomById(int roomId, string checkInDate, string checkOutDate)
        {

            var response = await _client.GetAsync($"api/hotelroom/{roomId}?checkIn={checkInDate}&checkOut={checkInDate}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var room = JsonConvert.DeserializeObject<HotelRoomDTO>(content);
                return room;
            }
            else
            {
                return null;
            }
        }
    }
}
