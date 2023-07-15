using BookingWizard.DAL.Entities.HotelRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces.IHotelRoomsRepo
{
    public interface IPhotoRoomsRepository
    {
        public void PhotoUpload(HotelRoom room);
        public void DeletePhoto(int id, int roomId);
        public RoomImages GetPhoto(int id);
    }
}
