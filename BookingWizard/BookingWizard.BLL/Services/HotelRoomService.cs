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
	public  class HotelRoomService : IHotelRoomService
	{
		IUnitOfWork _unitOfWork;
		IMapper _map;

		public HotelRoomService(IUnitOfWork unitOfWork, IMapper map)
		{
			_unitOfWork = unitOfWork;
			_map = map;
		}

		public hotelRoom Add(hotelRoom item, int id)
		{
			_unitOfWork.Rooms.Add(item, id);
			return item;
		}

		public void Delete(hotelRoom item)
		{
			_unitOfWork.Rooms.Delete(item);
		
		}

		public hotelRoom  Get(int id)
		{
			return _unitOfWork.Rooms.Get(id);

		}


		public IEnumerable<hotelRoom> GetAll(int id, string searchString = "")
		{
			if(!string.IsNullOrWhiteSpace(searchString)) 
			{
                return _unitOfWork.Rooms.GetAll(id, searchString);
            }
			return _unitOfWork.Rooms.GetAll(id);
		}

		public hotelRoom Update(hotelRoom item)
		{
			_unitOfWork.Rooms.Update(item);
			return item;
		}

		public void PhotoUpload(hotelRoom room)
		{
			_unitOfWork.Rooms.PhotoUpload(room);
		}

		public void DeletePhoto(int id)
		{
			_unitOfWork.Rooms.DeletePhoto(id);
		}
		public RoomImages GetPhoto(int id)
		{
			return _unitOfWork.Rooms.GetPhoto(id);
		}
	}
}
