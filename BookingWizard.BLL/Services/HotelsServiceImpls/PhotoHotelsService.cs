using AutoMapper;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services.HotelsServiceImpls
{
	public class PhotoHotelsService : IPhotoHotelsService
	{

		IUnitOfWork _unitOfWork;
		IMapper _map;
		public PhotoHotelsService(IUnitOfWork unitOfWork, IMapper map)
		{
			_unitOfWork = unitOfWork;
			_map = map;
		}

		public void DeletePhoto(int id, int HotelId)
		{
			_unitOfWork.PhotoHotels.DeletePhoto(id, HotelId);
		}

		public void PhotoUpload(Hotel hotel)
		{
			_unitOfWork.PhotoHotels.PhotoUpload(hotel);
		}

		public HotelImages GetPhoto(int id)
		{
			return _unitOfWork.PhotoHotels.GetPhoto(id);
		}
	}
}
