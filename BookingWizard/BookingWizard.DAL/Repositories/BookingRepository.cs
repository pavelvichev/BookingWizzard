using BookingWizard.DAL.Data;
using BookingWizard.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingWizard.DAL.Repositories
{

	public class BookingRepository : IBookingRepository
	{
		readonly BookingDbContext _context;

		public BookingRepository(BookingDbContext context)
		{
			_context = context;
		}

		public Entities.Booking Add(Entities.Booking item)
		{
			_context.Booking.Add(item);
			_context.SaveChanges();
			return item;
		}

		public Entities.Booking Delete(Entities.Booking item)
		{
			_context.Booking.Remove(item);
			_context.SaveChanges();
			return item;
		}

		public Entities.Booking Get(int id)
		{
			var item = _context.Booking.FirstOrDefault(x => x.Id == id);
			return item;
		}

		public IEnumerable<Entities.Booking> GetAll()
		{
			var items = _context.Booking.ToList();
			return items;
		}

		public Entities.Booking Update(Entities.Booking item)
		{
			_context.Booking.Update(item);
			return item;
		}
	}
}
