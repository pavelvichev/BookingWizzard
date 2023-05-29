using AutoMapper;
using BookingWizard.BLL.DTO;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services
{
	public class HotelService : IHotelService
	{
		IUnitOfWork _unitOfWork;
		IMapper _map;
		public HotelService(IUnitOfWork unitOfWork, IMapper map) 
		{
			_unitOfWork= unitOfWork;
			_map= map;
		}

		public HotelDTO Add(HotelDTO item)
		{
			_unitOfWork.Hotels.Add(_map.Map<Hotel>(item));
			return item;
		}

		public HotelDTO Delete(HotelDTO item)
		{
			_unitOfWork.Hotels.Delete(_map.Map<Hotel>(item));
			return item;
		}

		public HotelDTO Get(int id)
		{
			return _map.Map<HotelDTO>(_unitOfWork.Hotels.Get(id));

		}


		public IEnumerable<HotelDTO> GetAll()
		{
			return _map.Map<IEnumerable<HotelDTO>>(_unitOfWork.Hotels.GetAll());
		}

		public HotelDTO Update(HotelDTO item)
		{
			_unitOfWork.Hotels.Update(_map.Map<Hotel>(item));
			return item;
		}
	}
}
