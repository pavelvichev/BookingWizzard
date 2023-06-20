
using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces
{
	public interface IHotelRoomService
	{
		public hotelRoom Add(hotelRoom item, int hotelId);
		public void Delete(hotelRoom item);
		public hotelRoom Update(hotelRoom item);
		public hotelRoom Get(int Id);
		IEnumerable<hotelRoom> GetAll(int hotelId, string searchString = "");
	}
}
