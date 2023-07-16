using BookingWizard.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM.HotelRooms
{
    public class HotelRoomVM
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "RoomNameRequired")]
		public string Name { get; set; } 
		[Required(ErrorMessage = "RoomDescriptionRequired")]
		public string Description { get; set; }
        public int HotelId { get; set; }
		[Required(ErrorMessage = "RoomPriceRequired")]
		public short roomPricePerNight { get; set; } 
        public int NumberOfPeople { get; set; }

		[Required(ErrorMessage = "RoomImagesRequired")]
		public IEnumerable<IFormFile>? ImageModelList { get; set; }
        public RoomImagesVM? Image { get; set; }
        public IEnumerable<RoomImagesVM>? Images { get; set; }
        public bool isBooking { get; set; }
        public PrivilegesVM Privileges { get; set; }
        public IEnumerable<PrivilegesVM> PrivilegesList { get; set; }
    }
}
