using AutoMapper;
using BookingWizard.BLL.Interfaces.IBookingServices;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services.BookingServiceImpls
{
	public class BookingService : IBookingService
	{
		IUnitOfWork _unitOfWork;
		IMapper _map;
		private readonly IStringLocalizer<BookingService> _localizer;
		public BookingService(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<BookingService> localizer)
		{
			_unitOfWork = unitOfWork;
			_map = mapper;
			_localizer = localizer;

		}

		public uint CalcPrice(Booking item)
		{
			HotelRoom hotelRoom = _unitOfWork.Rooms.Get(item.RoomId); ;
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
				throw new Exception(_localizer["ArrivalLessDeparture"]);
			}
			else if (item.ArrivalDate.Date < DateTime.Now.Date)
			{
				throw new Exception(_localizer["ArrivalLessToday"]);
			}
			else if (item.DateOfDeparture.Date < DateTime.Now.Date)
			{
				throw new Exception(_localizer["DepartureLessToday"]);
			}
			else if (item.DateOfDeparture == item.ArrivalDate)
			{
				throw new Exception(_localizer["DepartureEqualsArrival"]);
			}

			_unitOfWork.Booking.Add(_map.Map<Booking>(item));
			return item;
		}

		public void Delete(int id)
		{
			_unitOfWork.Booking.Delete(id);
		}

		public Booking Get(int id)
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
