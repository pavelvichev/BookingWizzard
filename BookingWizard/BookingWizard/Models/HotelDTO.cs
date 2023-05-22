using System.ComponentModel.DataAnnotations;

namespace BookingWizard.Models
{
    public class HotelDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field can`t be null")]
        public string HotelName { get; set; } // навзание отеля
        [Required(ErrorMessage = "Field can`t be null")]
        public string HotelShortDescription { get; set; } // короткое описание отеля(на карточке)
        [Required(ErrorMessage = "Field can`t be null")]
        public string HotelLongDescription { get; set; } // общее описание (при нажатии)
       
        public ushort HotelMark { get; set; } // оценка
        public bool isFavourite { get; set; } // добавить в избраное

        public List<string> previlege; //привилегии
        public AddressDTO address { get; set; } // Аддрес

        
        public int addressId { get; set; } // айди адресса в таблице
        public string imageUrl { get; set; } // фото

        public hotelRoomDTO? room { get; set; }

        public List<hotelRoomDTO>? roomList { get; set; }
   



    }
}
