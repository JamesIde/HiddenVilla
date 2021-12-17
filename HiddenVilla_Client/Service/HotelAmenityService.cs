using HiddenVilla_Client.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class HotelAmenityService : IHotelAmenityService
    {
        private readonly HttpClient _client;

        public HotelAmenityService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<HotelAmenityDTO>> GetAmenityList()
        {
            // Get it from api. name is api[controller] which amenitycontroller
            var response = await _client.GetAsync($"api/amenity");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelAmenityDTO>>(content);
            return rooms;

        }
    }
}
