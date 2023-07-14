using BookingWizard.DAL.Entities.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces.IHotelsServices
{
	public interface IHotelService
	{
		public Hotel Add(Hotel item);
		public void Delete(Hotel item);
		public Hotel Update(Hotel item);
		public Hotel Get(int id);
		IEnumerable<Hotel> GetAll(string userId = "");
		public IEnumerable<Hotel> Search(string Address, float Lat, float Lng);

	}
}
