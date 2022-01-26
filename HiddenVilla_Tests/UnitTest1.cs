using Business.Repository.IRepository;
using HiddenVilla_API.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HiddenVilla_Tests
{
    public class TestHotelRoomController
    {
        private readonly HotelRoomController _controller;
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public TestHotelRoomController(HotelRoomController controller, IHotelRoomRepository hotelRoomRepository)
        {
            _controller = controller;
            _hotelRoomRepository = hotelRoomRepository;
        }
        [Fact]
        public async Task GetAllRooms()
        {
            var okResult = _controller.GetHotelRooms();
            Assert.IsTyp
        }
    }
}
