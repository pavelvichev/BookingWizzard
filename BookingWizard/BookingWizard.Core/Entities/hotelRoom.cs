using BookingWizard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Core.Entities
{
	public class hotelRoom
	{
		public int Id { get; set; }
		public string Name { get; set; } // имя номера

		public string Description { get; set; } // описание номера

		public Hotel? Hotel { get; set; }// инфо про отель
		public int HotelId { get; set; }

		public ushort roomPricePerNight { get; set; } // цена за ночь
		public Booking? booking { get; set; } // информация про бронь
		public int? bookingId { get; set; } // номер бронирования
		public string imageUrl { get; set; } // фото


		public bool isBooking { get; set; } // занят ли уже номер
	}
}
