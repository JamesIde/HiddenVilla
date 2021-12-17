using Business.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace HiddenVilla_API.Controllers
{


    [Route("api/[controller]")]
    public class AmenityController : Controller
    {
        private readonly IHotelAmenityRepository _hotelAmenityRepository;

        //Access IHotelAmenityRepo so we can access the HotelAmenityRepo methods like GetAllHotelAmenity

        public AmenityController( IHotelAmenityRepository hotelAmenityRepository)
        {
        _hotelAmenityRepository = hotelAmenityRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetAmenityList()
        {
            //Simple method of retrieving all amenities from db using the GetAllHotelAmenity method
            var amenityList = await _hotelAmenityRepository.GetAllHotelAmenity();
            return Ok(amenityList);
            
            
        } 
    }
}
