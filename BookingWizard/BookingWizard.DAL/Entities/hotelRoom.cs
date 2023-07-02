using BookingWizard.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities
{
	public class hotelRoom
	{
		public int Id { get; set; }
		public string Name { get; set; } // имя номера

		public string Description { get; set; } // описание номера

		public Hotel? Hotel { get; set; }// инфо про отель
		public int HotelId { get; set; }

		public ushort roomPricePerNight { get; set; } // цена за ночь
		public ICollection<Booking>? Bookings { get; set; } // информация про бронь

        [NotMapped]
        public IEnumerable<IFormFile> ImageModelList { get; set; }
        public ICollection<RoomImages> Images { get; set; }


        public bool isBooking { get; set; } // занят ли уже номер
	}
}
