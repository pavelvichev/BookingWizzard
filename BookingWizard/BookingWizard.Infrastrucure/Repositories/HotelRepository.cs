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
	public class HotelRepository : IHotelRepository<Hotel>
	{
		readonly AppDbContext _context;
		public HotelRepository(AppDbContext context)
		{
			_context = context;
		}
		public Hotel Add(Hotel item)
		{
			_context.hotels.Add(item);
			_context.SaveChanges();
			return item;
		}

		public Hotel Delete(Hotel item)
		{
			_context.hotels.Remove(item);
			_context.SaveChanges();
			return item;
		}

		public Hotel Get(int id)
		{
			Hotel hotel = _context.hotels.FirstOrDefault(h => h.Id == id);
			hotel.address = _context.Address.FirstOrDefault(x => hotel.addressId == x.Id);
			
			return hotel;
		}
		

		public IEnumerable<Hotel> GetAll()
		{
			var all = (from h in _context.hotels select h).ToList();
			return all;

		}

		public Hotel Update(Hotel item)
		{
			_context.Update(item);
			_context.SaveChanges();
			return item;
		}
	}
}
