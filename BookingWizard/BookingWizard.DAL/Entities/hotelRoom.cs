using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities
{
	public class hotelRoom
	{
		public int Id { get; set; }
		public int Number { get; set; } // имя номера

		public string Description { get; set; } // описание номера

		public Hotel? Hotel { get; set; }// инфо про отель
		public int HotelId { get; set; }

		public ushort roomPricePerNight { get; set; } // цена за ночь
		public ICollection<Booking>? Bookings { get; set; } // информация про бронь
		public string imageUrl { get; set; } // фото


		public bool isBooking { get; set; } // занят ли уже номер
	}
}
