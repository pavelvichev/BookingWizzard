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
			TimeSpan time = item.date_of_departure.Date - item.arrival_date.Date;
			uint sum;
			sum = hotelRoom.roomPricePerNight;
			 sum = sum * (uint)time.TotalDays;
			return sum;
        }

		 public Booking Add(Booking item)
		{
			if (item.arrival_date > item.date_of_departure)
			{
				throw new ValidationException("Arrival date can`t be less the date of deaprture","");
			}
			else if (item.arrival_date.Date < DateTime.Now.Date)
			{
				throw new ValidationException("Arrival date can`t be less the today`s date","");
			}
			else if (item.date_of_departure.Date < DateTime.Now.Date)
			{
				throw new ValidationException("Date o departure can`t be less the today`s date", "");
			}
			else if(item.date_of_departure == item.arrival_date)
			{
                throw new ValidationException("Date of departure can`t be equals the arrival date", "");
            }

			_unitOfWork.Booking.Add(_map.Map<Booking>(item));
			return item;
		}

		public void Delete(Booking item)
		{
			_unitOfWork.Booking.Delete(_map.Map<Booking>(item));
		}

		public Booking  Get(int id)
		{
			return _unitOfWork.Booking.Get(id);

		}

		public IEnumerable<Booking> GetAll()
		{
			return _unitOfWork.Booking.GetAll();
		}

		public Booking Update(Booking item)
		{
			_unitOfWork.Booking.Update(item);
			return item;
		}

       
    }
}
