using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
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

		public Hotel Add(Hotel item)
		{
			_unitOfWork.Hotels.Add(item);
			return item;
		}

		public void Delete(Hotel item)
		{
			_unitOfWork.Hotels.Delete(item);
			
		}

		public Hotel Get(int id)
		{
			return _unitOfWork.Hotels.Get(id);

		}


		public IEnumerable<Hotel> GetAll(string name = "")
		{
			if (name != null)
				return _unitOfWork.Hotels.GetAll(name);

			return _unitOfWork.Hotels.GetAll();
		}

		public Hotel Update(Hotel item)
		{
			_unitOfWork.Hotels.Update(item);
			return item;
		}

		public void DeletePhoto(int id)
		{
			_unitOfWork.Hotels.DeletePhoto(id);
		}

		public void PhotoUpload(Hotel hotel)
		{
			_unitOfWork.Hotels.PhotoUpload(hotel);
		}

		public HotelImages GetPhoto(int id)
		{
			return _unitOfWork.Hotels.GetPhoto(id);
		}

		public IEnumerable<Hotel> Search(string Address, float Lat, float Lng)
		{
			return	_unitOfWork.Hotels.Search(Address, Lat, Lng);
		}

	}
}
