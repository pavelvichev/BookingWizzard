using BookingWizard.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM
{
	public class hotelRoomVM
	{
		public int Id { get; set; }

		//[RegularExpression("^[\\p{L}]+$", ErrorMessage = "Incorect format")]
		public uint Number { get; set; } // имя номера

		public string Description { get; set; } // описание номера

		public BookingVM? booking { get; set; }
		public uint HotelId { get; set; }
		public ushort roomPricePerNight { get; set; } // цена за ночь

		public uint? bookingId { get; set; } // номер бронирования
		public string imageUrl { get; set; } // фото


		public bool isBooking { get; set; } // занят ли уже номер
	}
}
