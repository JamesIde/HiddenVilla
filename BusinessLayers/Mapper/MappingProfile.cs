using AutoMapper;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomDTO>();
            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
            CreateMap<HotelAmenity, HotelAmenityDTO>();
            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ForMember(x => x.HotelRoomDTO, opt => opt.MapFrom(c => c.HotelRoom));
            CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();
        }
    }
}
