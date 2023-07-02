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
			
			item.HotelId = hotelId;
			_context.hotelRooms.Add(item);
			_context.SaveChanges();
            PhotoUpload(item);
            return item;
		}

		public hotelRoom Delete(hotelRoom item)
		{
			
			_context.hotelRooms.Remove(item);
            foreach (var model in item.Images)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.Name);
                System.IO.File.Delete(filePath);
            }
           
            _context.SaveChanges();
			return item;
		}

		public hotelRoom Get(int id)
		{

			hotelRoom room = _context.hotelRooms.FirstOrDefault(r => r.Id == id);
			room.Images = _context.RoomImages.Where(u => u.RoomId == id).AsNoTracking().ToList();


			return room;

		}

		public IEnumerable<hotelRoom> GetAll(int hotelId, string searchString = "")
		{
			List<hotelRoom> all = null;

			if (!string.IsNullOrWhiteSpace(searchString))
			{
				 all = _context.hotelRooms.Where(x => x.Name.Contains(searchString) && x.HotelId == hotelId).ToList();
				
			}
			else
			{
				 all = (from h in _context.hotelRooms where h.HotelId == hotelId select h).ToList();
			}
            foreach (var item in all)
            {
                item.Images = _context.RoomImages.Where(u => u.RoomId == item.Id).AsNoTracking().ToList();
            }
            return all;
        }

		public hotelRoom Update(hotelRoom item)
		{
			
			_context.Update(item);
			_context.SaveChanges();
			return item;
		}



        public void PhotoUpload(hotelRoom room, int id = 0)
        {
            string uniqueFileName = null;
            string filePath = null;

            if (room.ImageModelList.All(x => x != null))
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                foreach (var item in room.ImageModelList)
                {
                    if (item != null)
                    {
                        uniqueFileName = Guid.NewGuid().ToString() + room.Name + "-" + item.FileName;
                        filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fs = new FileStream(filePath, FileMode.Create))
                        {
                            item.CopyTo(fs);
                        }

                        var newImage = new RoomImages
                        {
                            Name = uniqueFileName,
                            RoomId = room.Id
                        };

                        _context.RoomImages.Add(newImage);
                        _context.SaveChanges(); // Сохранение каждого экземпляра отдельно
                    }
                }
            }
        }

        public void DeletePhoto(string photoName)
        {
            var item = _context.RoomImages.Select(u => u).Where(x => x.Name.Equals(photoName)).FirstOrDefault();

            _context.RoomImages.Remove(item);
            _context.SaveChanges();
        }


    }
}

