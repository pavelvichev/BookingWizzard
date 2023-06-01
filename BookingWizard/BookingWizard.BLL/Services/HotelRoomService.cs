using AutoMapper;
using BookingWizard.BLL.DTO;
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

		public hotelRoomDTO Add(hotelRoomDTO item, int id)
		{
			_unitOfWork.Rooms.Add(_map.Map<hotelRoom>(item), id);
			return item;
		}

		public hotelRoomDTO Delete(hotelRoomDTO item)
		{
			_unitOfWork.Rooms.Delete(_map.Map<hotelRoom>(item));
			return item;
		}

		public hotelRoomDTO Get(int id)
		{
			return _map.Map<hotelRoomDTO>(_unitOfWork.Rooms.Get(id));

		}


		public IEnumerable<hotelRoomDTO> GetAll(int id, string searchString = "")
		{
			if(!string.IsNullOrWhiteSpace(searchString)) 
			{
                return _map.Map<IEnumerable<hotelRoomDTO>>(_unitOfWork.Rooms.GetAll(id, searchString));
            }
			return _map.Map<IEnumerable<hotelRoomDTO>>(_unitOfWork.Rooms.GetAll(id));
		}

		public hotelRoomDTO Update(hotelRoomDTO item)
		{
			_unitOfWork.Rooms.Update(_map.Map<hotelRoom>(item));
			return item;
		}
	}
}
