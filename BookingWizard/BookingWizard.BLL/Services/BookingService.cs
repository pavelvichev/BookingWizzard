using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services
{
	public class BookingService : IBookingService
	{
		IUnitOfWork _unitOfWork;
		IMapper _map;
		public BookingService(IUnitOfWork unitOfWork, IMapper mapper) 
		{
			_unitOfWork = unitOfWork;
			_map = mapper;
		}

        public uint CalcPrice(Booking item)
        {
			hotelRoom hotelRoom = _unitOfWork.Rooms.Get(item.RoomId); ;
			TimeSpan time = item.DateOfDeparture.Date - item.ArrivalDate.Date;
			uint sum;
			sum = hotelRoom.RoomPricePerNight;
			 sum = sum * (uint)time.TotalDays;
			return sum;
        }

		 public Booking Add(Booking item)
		{
			if (item.ArrivalDate > item.DateOfDeparture)
			{
				throw new Exception("Arrival date can`t be less the date of deaprture");
			}
			else if (item.ArrivalDate.Date < DateTime.Now.Date)
			{
				throw new Exception("Arrival date can`t be less the today`s date");
			}
			else if (item.DateOfDeparture.Date < DateTime.Now.Date)
			{
				throw new Exception("Date o departure can`t be less the today`s date");
			}
			else if(item.DateOfDeparture == item.ArrivalDate)
			{
                throw new Exception("Date of departure can`t be equals the arrival date");
            }
			
			_unitOfWork.Booking.Add(_map.Map<Booking>(item));
			return item;
		}

		public void Delete(int id)
		{
			_unitOfWork.Booking.Delete(id);
		}

		public Booking  Get(int id)
		{
			return _unitOfWork.Booking.Get(id);

		}

		public IEnumerable<Booking> GetAll(string currentUserId)
		{
			return _unitOfWork.Booking.GetAll(currentUserId);
		}

		public Booking Update(Booking item)
		{
			_unitOfWork.Booking.Update(item);
			return item;
		}

       
    }
}
