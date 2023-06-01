using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM
{
	public class AddressVM
	{
		public uint Id { get; set; }
		[RegularExpression("^[\\p{L} ]+$", ErrorMessage = "Incorect format")]
		public string Country { get; set; }
		[RegularExpression("^[\\p{L} ]+$", ErrorMessage = "Incorect format")]
		public string City { get; set; }
		[RegularExpression("^[\\p{L} ]+$", ErrorMessage = "Incorect format")]
		public string Region { get; set; }
		public string street { get; set; }
		public string PostalCode { get; set; }



	}
}
