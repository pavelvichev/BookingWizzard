using BookingWizard.BLL.DTO;
using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces
{
	public interface IBookingService
	{
		public BookingDTO Add(BookingDTO item);
		public BookingDTO Delete(BookingDTO item);
		public BookingDTO Update(BookingDTO item);
		public BookingDTO Get(int id);

		public uint CalcPrice(BookingDTO item);
		IEnumerable<BookingDTO> GetAll();
	}
}
