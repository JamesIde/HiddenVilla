using Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service.IService
{
    public interface IHotelAmenityService
    {

        public Task<IEnumerable<HotelAmenityDTO>> GetAmenityList();
    }
}
