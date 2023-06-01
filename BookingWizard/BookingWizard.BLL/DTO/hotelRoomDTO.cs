using BookingWizard.DAL.Entities;

namespace BookingWizard.BLL.DTO
{
    public class hotelRoomDTO
    {
        public int Id { get; set; } 
        public int Number { get; set; } // имя номера

        public string Description { get; set; } // описание номера

        public int HotelId { get; set;}
        public ushort roomPricePerNight { get; set; } // цена за ночь
       
        public int? bookingId { get; set; } // номер бронирования
        public string imageUrl { get; set; } // фото


      public  bool isBooking { get; set; } // занят ли уже номер
    }
}
