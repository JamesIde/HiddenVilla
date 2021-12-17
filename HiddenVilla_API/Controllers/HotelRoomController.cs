using Business.Repository.IRepository;
using CommonFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace HiddenVilla_API.Controllers
{
    //Purpose of this controller is to fetch the rooms from the database and return them to the client home page of the application.
    [Route("api/[controller]")]
    public class HotelRoomController : Controller
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(string checkIn = null, string checkOut = null)
        {
            if (string.IsNullOrEmpty(checkIn) || string.IsNullOrEmpty(checkOut))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if (!DateTime.TryParseExact(checkIn, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid date - Please do it as dd/MM/yyyy"
                });
            }
            if (!DateTime.TryParseExact(checkOut, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid date - Please do it as dd/MM/yyyy"
                });
            }

            var RoomCount = await _hotelRoomRepository.GetAllHotelRoom(checkIn,checkOut);
            return Ok(RoomCount);
        }


        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int? roomId, string checkIn = null, string checkOut = null)
        {
            if (string.IsNullOrEmpty(checkIn) || string.IsNullOrEmpty(checkOut))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if (!DateTime.TryParseExact(checkIn, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid date - Please do it as dd/MM/yyyy"
                });
            }
            if (!DateTime.TryParseExact(checkOut, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid date - Please do it as dd/MM/yyyy"
                });
            }

            var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkIn,checkOut);

            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "Error",
                    ErrorMessage = "InvalidRoomId",
                    StatusCode = StatusCodes.Status404NotFound
                }); ;
            }
            return Ok(roomDetails);
        }

    }
}
