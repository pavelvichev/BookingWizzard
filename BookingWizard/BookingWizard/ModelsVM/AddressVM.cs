using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM
{
	public class AddressVM
	{	
		public string AddressName { get; set; }

		[ModelBinder(typeof(BookingWizard.Infrastructure.AddressFloatModelBinder))] 
		public float Lng { get; set; }

		[ModelBinder(typeof(BookingWizard.Infrastructure.AddressFloatModelBinder))] // Применяем кастомный байндер для свойства Lat
		public float Lat { get; set; }
		public int HotelId { get; set; }
	}
}
