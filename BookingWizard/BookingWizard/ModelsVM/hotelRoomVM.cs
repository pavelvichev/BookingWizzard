using BookingWizard.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		public static List<int> NumbersOfPeople { get; set; } = new List<int>() { 1,2,3,4,5};
	
		public uint? bookingId { get; set; } // номер бронирования
		public uint NumberOfPeople { get; set; } // номер бронирования
        public IEnumerable<IFormFile>? ImageModelList { get; set; }
        public RoomImagesVM? Image { get; set; }
        public IEnumerable<RoomImagesVM>? Images { get; set; }
        public bool isBooking { get; set; } // занят ли уже номер
	}
}
