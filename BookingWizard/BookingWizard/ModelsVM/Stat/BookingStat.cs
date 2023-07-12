using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.ModelsVM.HotelRooms;
using BookingWizard.ModelsVM.Hotels;

namespace BookingWizard.ModelsVM
{
	public class BookingStat // Отчет про бронь на каждый день
	{
		public HotelRoomVM HotelRoom { get; set; } 
		public DateTime Date { get; set; }
		public int BookingsCount { get; set; }

	}
}
