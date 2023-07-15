using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Entities.HotelRooms;

namespace BookingWizard.DAL.Repositories.HotelRoomsRepo
{
    public class HotelRoomRepository : IHotelRoomsRepository
    {

        readonly BookingDbContext _context;
        readonly IPhotoRoomsRepository _photoRoomsRepository;

        public HotelRoomRepository(BookingDbContext context, IPhotoRoomsRepository photoRoomsRepository)
        {
            _context = context;
            _photoRoomsRepository = photoRoomsRepository;
        }
        public HotelRoom Add(HotelRoom item, int hotelId)
        {

            item.HotelId = hotelId;

            _context.HotelRooms.Add(item);
            _context.SaveChanges();
            _photoRoomsRepository.PhotoUpload(item);


			return item;

        }

        public HotelRoom Delete(int id)
        {
            var item = _context.HotelRooms.FirstOrDefault(x => x.Id == id);
            _context.HotelRooms.Remove(item);
            _context.SaveChanges();
            return item;
        }
        
        public HotelRoom Get(int id)
        {
			HotelRoom room = _context.HotelRooms
						.Include(r => r.Images) 
						.Include(r => r.Hotel) 
						.FirstOrDefault(r => r.Id == id);

            room.Image = room.Images.FirstOrDefault();

            return room;

        }

        public IEnumerable<HotelRoom> GetAll(int hotelId, int NumberOfPeople = 0)
        {
			IQueryable<HotelRoom> query = _context.HotelRooms.Include(r => r.Images);

			if (NumberOfPeople != 0)
			{
				query = query.Where(r => r.NumberOfPeople >= NumberOfPeople);
			}
			else
			{
				query = query.Where(r => r.HotelId == hotelId);
			}

			List<HotelRoom> all = query.ToList();

			foreach (var item in all)
			{
				item.Image = item.Images.FirstOrDefault();
			}
			return all;

        }

        public HotelRoom Update(HotelRoom item)
        {

            _context.Update(item);
            _context.SaveChanges();
            return item;
        }


    }
}

