using AutoMapper;
using Business.Repository.IRepository;
using CommonFiles;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoomOrderDetailsRepository : IRoomOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RoomOrderDetailsRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }



        public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
        {
            try
            {

                //Creating order detail, this will be a booking
                //Get the dates
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate = details.CheckOutDate.Date;

                //Convert DTO to return RoomOrderDetails object
                var roomOrder = _mapper.Map<RoomOrderDetailsDTO, RoomOrderDetails>(details);
                roomOrder.Status = SD.Status_Pending;
                var result = await _db.RoomOrderDetails.AddAsync(roomOrder);

                await _db.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(result.Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailsDTO>> GetAllRoomOrderDetail()
        {
            try
            {

                IEnumerable<RoomOrderDetailsDTO> roomOrders = _mapper.Map<IEnumerable<RoomOrderDetails>, IEnumerable<RoomOrderDetailsDTO>>
                   (_db.RoomOrderDetails.Include(u => u.HotelRoom));

                return roomOrders;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RoomOrderDetailsDTO> GetRoomOrderDetail(int roomOrderId)
        {
            try
            {

                RoomOrderDetails roomOrder = await _db.RoomOrderDetails.Include(u => u.HotelRoom).ThenInclude(x => x.HotelRoomImages).FirstOrDefaultAsync(u => u.Id == roomOrderId);

                RoomOrderDetailsDTO roomOrderDetailsDTO = _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(roomOrder);
                roomOrderDetailsDTO.HotelRoomDTO.StayLength = roomOrderDetailsDTO.CheckOutDate.Subtract(roomOrderDetailsDTO.CheckInDate).Days;
                return roomOrderDetailsDTO;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<bool> OrderStatus(int roomOrderId, string status)
        {
            try
            {

                var order = await _db.RoomOrderDetails.FirstOrDefaultAsync(u => u.Id == roomOrderId);
                if (order == null)
                {
                    return false;
                }
                else
                {
                    order.Status = status;
                }

                if (status == SD.Status_CheckedIn)
                {
                    order.ActualCheckInDate = DateTime.Now;
                }
                if (status == SD.Status_CheckedOut_Completed)
                {
                    order.ActualCheckOutDate = DateTime.Now;
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<RoomOrderDetailsDTO> PaymentSuccessful(int id)
        {
            var data = await _db.RoomOrderDetails.FindAsync(id);

            if (data == null)
            {
                return null;
            }

            if (!data.PaymentSuccessful)
            {
                data.PaymentSuccessful = true;
                data.Status = SD.Status_Booked;
                var paymentSuccess = _db.RoomOrderDetails.Update(data);
                await _db.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetailsDTO>(paymentSuccess.Entity);
            }
            return new RoomOrderDetailsDTO();
        }


        public async Task<int> DeleteBooking(int bookingId)
        {

            //Find the booking 
            var booking = await _db.RoomOrderDetails.FindAsync(bookingId);

            if (booking == null)
            {
                return 0;
            }

            _db.RoomOrderDetails.Remove(booking);
            return await _db.SaveChangesAsync();

        }
    }
}

