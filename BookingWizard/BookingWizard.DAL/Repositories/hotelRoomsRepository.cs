using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using BookingWizard.DAL.Entities;

namespace BookingWizard.DAL.Repositories
{
    public class hotelRoomsRepository : IHotelRoomRepository<hotelRoom>
	{

		readonly BookingDbContext _context;
	

		public hotelRoomsRepository(BookingDbContext context)
		{
			_context = context;
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
            _context.SaveChanges();
			return item;
		}

		public hotelRoom Get(int id)
		{

			hotelRoom room = _context.hotelRooms.FirstOrDefault(r => r.Id == id);
			room.Images = _context.RoomImages.Where(u => u.RoomId == id).AsNoTracking().ToList();
			Hotel hotel = _context.hotels.FirstOrDefault(x => x.Id == room.HotelId);
			room.Hotel = hotel;


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



        public void PhotoUpload(hotelRoom room)
        {
            foreach (var file in room.ImageModelList)
            {
                if (file != null && file.Length > 0)
                {
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + room.Name + "-" + file.FileName;
                    var photo = new RoomImages
                    {
                        Name = uniqueFileName,
                        ImageData = imageData,
                        RoomId = room.Id
                    };

                    _context.RoomImages.Add(photo);
                    _context.SaveChanges();

                }
            }

        }

        public void DeletePhoto(int id)
        {
            var item = _context.RoomImages.Select(u => u).Where(x => x.Id == id).FirstOrDefault();

            _context.RoomImages.Remove(item);
            _context.SaveChanges();
        }

		public RoomImages GetPhoto(int id)
		{
			var photo = _context.RoomImages.FirstOrDefault(p => p.Id == id);


			return photo; // ил			
		}

    }
}

