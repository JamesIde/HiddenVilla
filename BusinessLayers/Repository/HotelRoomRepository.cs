using AutoMapper;
using Business.Repository.IRepository;
using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        //implements the methods defined in the interface
        

        // Use application db context
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        // get an implementation of appdbcontext and assign to local variable 
        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }


        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";

            var addRoom = await _db.HotelRooms.AddAsync(hotelRoom);

            await _db.SaveChangesAsync();

            return _mapper.Map<HotelRoom, HotelRoomDTO>(addRoom.Entity);

        }

        public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRoom(string checkInStr, string checkOutStr)
        {
            try
            {
                IEnumerable<HotelRoomDTO> hotelRoomDTOs = _mapper.Map<IEnumerable<HotelRoom>, 
                    IEnumerable<HotelRoomDTO>>(_db.HotelRooms.Include(x => x.HotelRoomImages));

                if (!string.IsNullOrEmpty(checkInStr) && !string.IsNullOrEmpty(checkOutStr)){
                   foreach(HotelRoomDTO hotelRoom in hotelRoomDTOs)
                    {
                        hotelRoom.IsBooked = await IsBooked(hotelRoom.Id, checkInStr, checkOutStr);

                    }

                }
                return hotelRoomDTOs;
            }

            catch (Exception ex )
            {
                {
                    throw ex;
                }
            }
        }


        public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInStr, string checkOutStr)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _db.HotelRooms.Include(x=>x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));

                if(!string.IsNullOrEmpty(checkInStr)&& !string.IsNullOrEmpty(checkOutStr)){
                    hotelRoom.IsBooked = await IsBooked(hotelRoom.Id, checkInStr, checkOutStr);
                }

                return hotelRoom;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        
        public async Task<bool> IsBooked(int RoomId, string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkOutDatestr) && !string.IsNullOrEmpty(checkInDatestr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDatestr, "dd/MM/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDatestr, "dd/MM/yyyy", null);

                    var existingBooking = await _db.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.PaymentSuccessful &&
                       //check if checkin date that user wants does not fall in between any dates for room that is booked
                       (checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate
                       //check if checkout date that user wants does not fall in between any dates for room that is booked
                       || checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date
                       )).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomId == hotelRoomDTO.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(roomId);

            if (roomDetails != null)
            {
                //find the images associated with the roomId
                var allImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
                

                _db.HotelRoomImages.RemoveRange(allImages);

                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;

        }
    }
}
