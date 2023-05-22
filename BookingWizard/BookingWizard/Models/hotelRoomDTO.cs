namespace BookingWizard.Models
{
    public class hotelRoomDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; } // имя номера

        public string Description { get; set; } // описание номера

      
        public ushort roomPricePerNight { get; set; } // цена за ночь
        public BookingDTO? booking { get; set; } // информация про бронь
        public int? bookingId { get; set; } // номер бронирования
        public string imageUrl { get; set; } // фото


      public  bool isBooking { get; set; } // занят ли уже номер
    }
}
