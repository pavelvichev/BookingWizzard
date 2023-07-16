using System.ComponentModel.DataAnnotations;
using BookingWizard.ModelsVM.HotelRooms;

namespace BookingWizard.ModelsVM.Hotels
{
    public class HotelVM
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "HotelNameRequired")]
		public string HotelName { get; set; }
		[Required(ErrorMessage = "HotelShortDescriptionRequired")]
		public string HotelShortDescription { get; set; }
		[Required(ErrorMessage = "HotelLongDescriptionRequired")]
		public string HotelLongDescription { get; set; }
        public ushort HotelMark { get; set; } 
        public AddressVM Address { get; set; }
        public int AddressId { get; set; }
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
