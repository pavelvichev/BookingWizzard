using BookingWizard.DAL.Entities.HotelRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces.IBookingServices
{
	public interface IBookingService
	{
		public Booking Add(Booking item);
		public void Delete(int id);
		public Booking Update(Booking item);
		public Booking Get(int id);

		public uint CalcPrice(Booking item);
		IEnumerable<Booking> GetAll(string currentUserId);
	}
}
