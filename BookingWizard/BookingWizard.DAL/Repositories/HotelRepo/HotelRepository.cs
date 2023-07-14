using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookingWizard.DAL.Entities;
using Microsoft.Extensions.Localization;
using BookingWizard.DAL.Interfaces.IHotelRepo;
using BookingWizard.DAL.Entities.Hotels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookingWizard.DAL.Repositories.HotelRepo
{
    public class HotelRepository : IHotelRepository<Hotel>
    {
        readonly BookingDbContext _context;
        readonly IPhotoHotelsRepository _photoHotelsRepository;
        private readonly IStringLocalizer<HotelRepository> _localizer;
        public HotelRepository(BookingDbContext context, IStringLocalizer<HotelRepository> localizer, IPhotoHotelsRepository photoHotelsRepository)
        {
            _context = context;
            _localizer = localizer;
            _photoHotelsRepository = photoHotelsRepository;
        }
        public Hotel Add(Hotel item)
        {

            _context.hotels.Add(item);
            _context.SaveChanges();
            _photoHotelsRepository.PhotoUpload(item);

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
			Hotel hotel = _context.hotels
						.Include(h => h.Address)
						.Include(h => h.Images) 
						.FirstOrDefault(h => h.Id == id);

            hotel.Image = hotel.Images.FirstOrDefault();


            return hotel;
        }


        public IEnumerable<Hotel> GetAll(string userId = "")
        {
			IQueryable<Hotel> query = _context.hotels.Include(h => h.Address).Include(h => h.Images);

			if (!string.IsNullOrWhiteSpace(userId))
			{
				query = query.Where(h => h.IdentityUserId.Contains(userId));
			}

			List<Hotel> all = query.ToList();
			foreach (var item in all)
			{

                item.Image = item.Images.FirstOrDefault();
			
            }
            return all;

        }

        public Hotel Update(Hotel item)
        {
            _context.Update(item);
            _context.SaveChanges();
            return item;
        }



        public IEnumerable<Hotel> Search(string Address, float Lat, float Lng)
        {
            var sourceCoordinates = new Coordinates(Lat, Lng);


            var hotels = GetAll();


            var hotelsWithinRadius = new List<Hotel>();
            if (Lat == 0 && Lng == 0)
            {
                hotelsWithinRadius = GetAll().ToList();
            }
            else
            {
                foreach (var hotel in hotels)
                {

                    var destinationCoordinates = new Coordinates(hotel.Address.Lat, hotel.Address.Lng);
                    var distance = CalculateDistance(sourceCoordinates, destinationCoordinates);


                    if (distance < 700)
                    {
                        hotelsWithinRadius.Add(hotel);
                    }

                }
            }
            if (hotelsWithinRadius.Count() == 0)
            {
                hotelsWithinRadius = GetAll().ToList();

                throw new Exception(_localizer["noHotels"]);
            }



            return hotelsWithinRadius;
        }

        public float CalculateDistance(Coordinates source, Coordinates destination)
        {
            const float EarthRadius = 6371;

            var latDifference = ToRadians(destination.Latitude - source.Latitude);
            var lngDifference = ToRadians(destination.Longitude - source.Longitude);

            var a = Math.Sin(latDifference / 2) * Math.Sin(latDifference / 2) +
                    Math.Cos(ToRadians(source.Latitude)) * Math.Cos(ToRadians(destination.Latitude)) *
                    Math.Sin(lngDifference / 2) * Math.Sin(lngDifference / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = EarthRadius * c;

            return (float)distance;
        }

        public float ToRadians(float degree)
        {
            return (float)(degree * Math.PI / 180);
        }


    }
}
