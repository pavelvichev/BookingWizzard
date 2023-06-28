using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace BookingWizard.DAL.Repositories
{
	public class HotelRepository : IHotelRepository<Hotel>
	{
		readonly BookingDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public HotelRepository(BookingDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}
		public Hotel Add(Hotel item)
		{
			PhotoUpload(item);
			_context.hotels.Add(item);

			_context.SaveChanges();
			return item;
		}

		public Hotel Delete(Hotel item)
		{
			if (item.Image != null)
			{
				string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", item.Image);
				System.IO.File.Delete(filePath);
			}
			_context.hotels.Remove(item);
			_context.SaveChanges();
			return item;
		}

		public Hotel Get(int id)
		{ 
			Hotel hotel = _context.hotels.AsNoTracking().FirstOrDefault(h => h.Id == id);
			hotel.address = _context.Address.AsNoTracking().FirstOrDefault(x => hotel.Id == x.HotelId);
			
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
			PhotoUpload(item);
			_context.Update(item);
			_context.SaveChanges();
			return item;
		}

		public void PhotoUpload(Hotel hotel)
		{
			if (hotel.ImageModel.Image != null)
			{
				if (hotel.Image != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", hotel.Image);
					System.IO.File.Delete(filePath);
				}
			}

			hotel.Image = ProcesUploadedHotelFile(hotel);
		}

		string ProcesUploadedHotelFile(Hotel hotel)
		{
			string uniqueFileName = null;
			string filePath = null;
			if (hotel.ImageModel.Image != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "-" + hotel.ImageModel.Image.FileName;
				filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fs = new FileStream(filePath, FileMode.Create))
				{
					hotel.ImageModel.Image.CopyTo(fs);
				}
			}


			return uniqueFileName;
		}

		
	}
}
