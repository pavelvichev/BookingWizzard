using AutoMapper;
using BookingWizard.BLL.Interfaces.IHotelRoomsServices;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services.RoomsServiceImpls
{
	public class HotelRoomService : IHotelRoomService
	{
		IUnitOfWork _unitOfWork;
		IMapper _map;

		public HotelRoomService(IUnitOfWork unitOfWork, IMapper map)
		{
			_unitOfWork = unitOfWork;
			_map = map;
		}

		public HotelRoom Add(HotelRoom item, int id)
		{
			_unitOfWork.Rooms.Add(item, id);
			return item;
		}

		public void Delete(HotelRoom item)
		{
			_unitOfWork.Rooms.Delete(item);

		}

		public HotelRoom Get(int id)
		{
			return _unitOfWork.Rooms.Get(id);

		}


		public IEnumerable<HotelRoom> GetAll(int id, int NumberOfPeople = 0)
		{
			return _unitOfWork.Rooms.GetAll(id, NumberOfPeople);
		}

		public HotelRoom Update(HotelRoom item)
		{
			_unitOfWork.Rooms.Update(item);
			return item;
		}

	}
}
