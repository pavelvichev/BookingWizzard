using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Data;

namespace BookingWizard.DAL.Repositories
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
		

		public IEnumerable<Hotel> GetAll(string name = "")
		{
			if(!string.IsNullOrWhiteSpace(name))
			{
				var allSearch = _context.hotels.Where(x => x.HotelName.Contains(name));
			return allSearch;
			}
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
