using BookingWizard.BLL.DTO;
using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces
{
	public interface IHotelService
	{
		public HotelDTO Add(HotelDTO item);
		public HotelDTO Delete(HotelDTO item);
		public HotelDTO Update(HotelDTO item);
		public HotelDTO Get(int id);
		IEnumerable<HotelDTO> GetAll();
	}
}
