using BookingWizard.Core.Entities;
using BookingWizard.Core.Interfaces;
using BookingWizard.Infrastrucure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Infrastrucure.Repositories
{
	public class hotelRoomsRepository : IEntityRepository<hotelRoom>
	{

		readonly AppDbContext _context;

		public hotelRoomsRepository(AppDbContext context)
		{
			_context = context;
		}
		public hotelRoom Add(hotelRoom item)
		{
			_context.hotelRooms.Add(item);
			_context.SaveChanges();
			return item;
		}

		public hotelRoom Delete(hotelRoom item)
		{
			_context.hotelRooms.Remove(item);
			_context.SaveChanges();
			return item;
		}

		public hotelRoom Get(int id)
		{
			hotelRoom hotel = _context.hotelRooms.FirstOrDefault(h => h.Id == id);

			return hotel;
		}

		public IEnumerable<hotelRoom> GetAll()
		{
			var all = (from h in _context.hotelRooms select h).ToList();
			return all;

		}

		public hotelRoom Update(hotelRoom item)
		{
			_context.Update(item);
			_context.SaveChanges();
			return item;
		}

		
	}
}

