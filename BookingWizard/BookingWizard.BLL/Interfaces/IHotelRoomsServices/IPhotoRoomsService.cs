using BookingWizard.DAL.Entities.HotelRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces.IHotelRoomsServices
{
	public interface IPhotoRoomsService
	{
		public void DeletePhoto(int id, int roomId);
		public void PhotoUpload(HotelRoom room);
		public RoomImages GetPhoto(int id);
	}
}
