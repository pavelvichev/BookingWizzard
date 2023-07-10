using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces
{
    public interface IBookingRepository
	{
		public Booking Add(Booking item);
		public Booking Delete(int id);
		public Booking Update(Booking item);
		public Booking Get(int id);
		IEnumerable<Booking> GetAll(string currentUserId);
	}
}
