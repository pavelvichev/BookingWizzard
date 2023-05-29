using BookingWizard.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces
{
	public interface IHotelRoomService
	{
		public hotelRoomDTO Add(hotelRoomDTO item, int hotelId);
		public hotelRoomDTO Delete(hotelRoomDTO item);
		public hotelRoomDTO Update(hotelRoomDTO item);
		public hotelRoomDTO Get(int Id);
		IEnumerable<hotelRoomDTO> GetAll(int hotelId);
	}
}
