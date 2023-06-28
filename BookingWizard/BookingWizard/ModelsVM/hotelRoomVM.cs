using BookingWizard.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM
{
	public class hotelRoomVM
	{
		public int Id { get; set; }

		//[RegularExpression("^[\\p{L}]+$", ErrorMessage = "Incorect format")]
		public string Name { get; set; } // имя номера

		public string Description { get; set; } // описание номера

		public BookingVM? booking { get; set; }
		public uint HotelId { get; set; }
		public ushort roomPricePerNight { get; set; } // цена за ночь

		public uint? bookingId { get; set; } // номер бронирования
		public string? Image { get; set; } // фото

		public ImageModel ImageModel { get; set; }
		public bool isBooking { get; set; } // занят ли уже номер
	}
}
