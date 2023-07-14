using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM.Hotels
{
	public class ReviewVM
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "ReviewTextRequired")]
		public string Text { get; set; }
		[DataType(DataType.Date)]
		public DateTime ReviewDate { get; set; }
		[Range(1, 5)]
		public int Rating { get; set; }
		public string IdentityUserId { get; set; }
		public IdentityUser? User { get; set; }
		public int HotelId { get; set; }

		

	}
}
