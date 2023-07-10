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
using BookingWizard.DAL.Entities;
using Microsoft.Extensions.Localization;

namespace BookingWizard.DAL.Repositories
{
    public class HotelRepository : IHotelRepository<Hotel>
	{
		readonly BookingDbContext _context;
		private readonly IStringLocalizer<HotelRepository> _localizer;
		public HotelRepository(BookingDbContext context, IStringLocalizer<HotelRepository> localizer)
		{
			_context = context;
			_localizer= localizer;
	
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
     
            _context.hotels.Remove(item);
			_context.SaveChanges();
			return item;
		}

		public Hotel Get(int id)
		{ 
			Hotel hotel = _context.hotels.AsNoTracking().FirstOrDefault(h => h.Id == id);
			hotel.Address = _context.Address.AsNoTracking().FirstOrDefault(x => hotel.Id == x.HotelId);
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
				item.Address = _context.Address.Where(u => u.HotelId == item.Id).AsNoTracking().FirstOrDefault();
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


		public IEnumerable<Hotel> Search(string Address, float Lat, float Lng)
		{
            var sourceCoordinates = new Coordinates(Lat, Lng);

            // Получение списка отелей из модели (вы можете заменить его на соответствующий источник данных)
            var hotels = GetAll();
			  
            // Создание списка для хранения отелей в пределах 1000 км
            var hotelsWithinRadius = new List<Hotel>();
			if (Lat == 0 && Lng == 0)
			{
				hotelsWithinRadius = GetAll().ToList();
			}
			else
			{
				foreach (var hotel in hotels)
				{
					// Вычисление расстояния между заданными координатами и координатами отеля
					var destinationCoordinates = new Coordinates(hotel.Address.Lat, hotel.Address.Lng);
					var distance = CalculateDistance(sourceCoordinates, destinationCoordinates);

					// Если расстояние меньше 1000 км, добавить отель в список
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
            const float EarthRadius = 6371; // Радиус Земли в километрах

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
