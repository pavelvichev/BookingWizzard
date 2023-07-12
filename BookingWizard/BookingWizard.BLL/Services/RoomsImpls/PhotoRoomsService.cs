using AutoMapper;
using BookingWizard.BLL.Interfaces.IRooms;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services.RoomsImpls
{

    public class PhotoRoomsService : IPhotoRoomsService
    {
        IUnitOfWork _unitOfWork;
        IMapper _map;

        public PhotoRoomsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;


        }
        public void PhotoUpload(HotelRoom room)
        {
            _unitOfWork.PhotoRooms.PhotoUpload(room);
        }

        public void DeletePhoto(int id, int roomId)
        {
            _unitOfWork.PhotoRooms.DeletePhoto(id, roomId);
        }
        public RoomImages GetPhoto(int id)
        {
            return _unitOfWork.PhotoRooms.GetPhoto(id);
        }
    }
}
