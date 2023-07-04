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
using Microsoft.AspNetCore.Mvc;

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

        public  void  PhotoUpload(Hotel hotel)
        {
			foreach(var file in hotel.ImageModelList) {
                if (file != null && file.Length > 0)
                {
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                   string  uniqueFileName = Guid.NewGuid().ToString() + hotel.HotelName + "-" + file.FileName;
					var photo = new HotelImages
					{
						Name = uniqueFileName,
						ImageData = imageData,
						HotelId = hotel.Id
                    };

                    _context.HotelImages.Add(photo);
                    _context.SaveChanges();

                }
            }

        }

        public void DeletePhoto(int id)
		{

           
            var item = _context.HotelImages.Select(u => u).Where(x => x.Id == id).FirstOrDefault();

			_context.HotelImages.Remove(item);
			_context.SaveChanges();
		}

		public HotelImages GetPhoto(int id)
		{
			 var photo = _context.HotelImages.FirstOrDefault(p => p.Id == id);
		
            return photo;
        }

	}
}
