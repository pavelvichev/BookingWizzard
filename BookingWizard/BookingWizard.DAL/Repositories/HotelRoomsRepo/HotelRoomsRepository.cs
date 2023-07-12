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

            HotelRoom room = _context.hotelRooms.FirstOrDefault(r => r.Id == id);
            room.Images = _context.RoomImages.Where(u => u.RoomId == id).AsNoTracking().ToList();
            room.Image = _context.RoomImages.Where(u => u.RoomId == id).AsNoTracking().FirstOrDefault();
            Hotel hotel = _context.hotels.FirstOrDefault(x => x.Id == room.HotelId);
            room.Hotel = hotel;


            return room;

        }

        public IEnumerable<HotelRoom> GetAll(int hotelId, int NumberOfPeople = 0)
        {
            List<HotelRoom> all = null;

            if (NumberOfPeople != 0)
            {
                all = _context.hotelRooms.Where(x => x.NumberOfPeople >= NumberOfPeople).ToList();
            }
            else
            {
                all = (from h in _context.hotelRooms where h.HotelId == hotelId select h).ToList();
            }
            foreach (var item in all)
            {
                item.Images = _context.RoomImages.Where(u => u.RoomId == item.Id).AsNoTracking().ToList();
                item.Image = _context.RoomImages.Where(u => u.RoomId == item.Id).AsNoTracking().FirstOrDefault();
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

