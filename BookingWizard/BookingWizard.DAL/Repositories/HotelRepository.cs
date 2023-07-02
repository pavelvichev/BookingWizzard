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
using Microsoft.AspNetCore.Http;

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
			
			_context.hotels.Add(item);
			_context.SaveChanges();
			PhotoUpload(item);
			
			return item;
		}

		public Hotel Delete(Hotel item)
		{
            foreach (var model in item.Images)
            {
				string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.Name);
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
			hotel.Images = _context.HotelImages.Where(U => U.HotelId == id).AsNoTracking().ToList();
			
			
			return hotel;
		}
		

		public IEnumerable<Hotel> GetAll(string name = "")
		{
			List<Hotel> all = null;
			if (!string.IsNullOrWhiteSpace(name))
			{
				all = _context.hotels.Where(x => x.HotelName.Contains(name)).ToList();

			}
			else
			{
				all = (from h in _context.hotels select h).ToList();
			}
			foreach (var item in all)
			{
				item.Images = _context.HotelImages.Where(u=> u.HotelId == item.Id).AsNoTracking().ToList();
			}
			return all;

		}

		public Hotel Update(Hotel item)
		{
			_context.Update(item);
			_context.SaveChanges();
			return item;
		}



		public void PhotoUpload(Hotel hotel, int id = 0)
		{
			string uniqueFileName = null;
			string filePath = null;

			if (hotel.ImageModelList.All(x => x != null))
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

				foreach (var item in hotel.ImageModelList)
				{
					if (item != null)
					{
						uniqueFileName = Guid.NewGuid().ToString() + hotel.HotelName + "-" + item.FileName;
						filePath = Path.Combine(uploadsFolder, uniqueFileName);

						using (var fs = new FileStream(filePath, FileMode.Create))
						{
							item.CopyTo(fs);
						}

						var newImage = new HotelImages
						{
							Name = uniqueFileName,
							HotelId = hotel.Id
						};

						_context.HotelImages.Add(newImage);
						_context.SaveChanges(); // Сохранение каждого экземпляра отдельно
					}
				}
			}
		}

		public void DeletePhoto(string photoName)
		{

           
            var item = _context.HotelImages.Select(u => u).Where(x => x.Name.Equals(photoName)).FirstOrDefault();

			_context.HotelImages.Remove(item);
			_context.SaveChanges();
		}


	}
}
