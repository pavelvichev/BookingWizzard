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
    public class HotelRoomsRepository : IHotelRoomsRepository
    {

        readonly BookingDbContext _context;
        readonly IPhotoRoomsRepository _photoRoomsRepository;

        public HotelRoomsRepository(BookingDbContext context, IPhotoRoomsRepository photoRoomsRepository)
        {
            _context = context;
            _photoRoomsRepository = photoRoomsRepository;
        }
        public HotelRoom Add(HotelRoom item, int hotelId)
        {

            item.HotelId = hotelId;

            _context.hotelRooms.Add(item);
            _context.SaveChanges();
            _photoRoomsRepository.PhotoUpload(item);
            return item;
        }

        public HotelRoom Delete(HotelRoom item)
        {

            _context.hotelRooms.Remove(item);
            _context.SaveChanges();
            return item;
        }

        public HotelRoom Get(int id)
        {
			HotelRoom room = _context.hotelRooms
						.Include(r => r.Images) 
						.Include(r => r.Hotel) 
						.FirstOrDefault(r => r.Id == id);

            room.Image = room.Images.FirstOrDefault();

            return room;

        }

        public IEnumerable<HotelRoom> GetAll(int hotelId, int NumberOfPeople = 0)
        {
			IQueryable<HotelRoom> query = _context.hotelRooms.Include(r => r.Images);

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

