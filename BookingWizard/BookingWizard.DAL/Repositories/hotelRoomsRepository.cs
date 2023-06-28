using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace BookingWizard.DAL.Repositories
{
	public class hotelRoomsRepository : IHotelRoomRepository<hotelRoom>
	{

		readonly BookingDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public hotelRoomsRepository(BookingDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}
		public hotelRoom Add(hotelRoom item, int hotelId)
		{
			PhotoUpload(item);
			item.HotelId = hotelId;
			_context.hotelRooms.Add(item);
			_context.SaveChanges();
			return item;
		}

		public hotelRoom Delete(hotelRoom item)
		{
			if (item.Image != null)
			{
				string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", item.Image);
				if (item.Image != "noimage.png")
					System.IO.File.Delete(filePath);
			}
			_context.hotelRooms.Remove(item);
			_context.SaveChanges();
			return item;
		}

		public hotelRoom Get(int id)
		{

			hotelRoom room = _context.hotelRooms.FirstOrDefault(r => r.Id == id);


			return room;

		}

		public IEnumerable<hotelRoom> GetAll(int hotelId, string searchString = "")
		{
			if (!string.IsNullOrWhiteSpace(searchString))
			{
                var allSearch = _context.hotelRooms.Where(x => x.Name.ToString().Contains(searchString));
                return allSearch;
            }
			var all = (from h in _context.hotelRooms where h.HotelId == hotelId select h).ToList();
			return all;

		}

		public hotelRoom Update(hotelRoom item)
		{
			PhotoUpload(item);
			_context.Update(item);
			_context.SaveChanges();
			return item;
		}

		public void PhotoUpload(hotelRoom item)
		{
			if (item.ImageModel.Image != null)
			{
				if (item.Image != null)
				{
					string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", item.Image);
					System.IO.File.Delete(filePath);
				}
			}

			item.Image = ProcesUploadedRoomFile(item);
		}

		string ProcesUploadedRoomFile(hotelRoom item)
		{
			string uniqueFileName = null;
			string filePath = null;
			if (item.ImageModel.Image != null)
			{
				string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "-" + item.ImageModel.Image.FileName;
				filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fs = new FileStream(filePath, FileMode.Create))
				{
					item.ImageModel.Image.CopyTo(fs);
				}
			}

			return uniqueFileName;
		}
	}
}

