using System.ComponentModel.DataAnnotations;
using BookingWizard.ModelsVM.HotelRooms;

namespace BookingWizard.ModelsVM.Hotels
{
    public class HotelVM
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "HotelNameRequired")]
		public string HotelName { get; set; } // навзание отеля
		[Required(ErrorMessage = "HotelShortDescriptionRequired")]
		public string HotelShortDescription { get; set; } // короткое описание отеля(на карточке)
		[Required(ErrorMessage = "HotelLongDescriptionRequired")]
		public string HotelLongDescription { get; set; } // общее описание (при нажатии)
        public ushort HotelMark { get; set; } // оценка
        public bool IsFavourite { get; set; } // добавить в избраное
        public List<string> previlege; //привилегии
        public AddressVM Address { get; set; } // Аддрес
        public int AddressId { get; set; } // айди адресса в таблице
		[Required(ErrorMessage = "HotelsImagesRequired")]
		public IEnumerable<IFormFile>? ImageModelList { get; set; }
        public HotelImagesVM? Image { get; set; }
        public IEnumerable<HotelImagesVM>? Images { get; set; }
        public HotelRoomVM? Room { get; set; }
        public IEnumerable<HotelRoomVM>? roomList { get; set; }
        public string IdentityUserId { get; set; }
        public ReviewVM? ReviewVM { get; set; }
        public IEnumerable<ReviewVM>? AllReviews { get; set; }



    }
}
