using BookingWizard.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM.Hotels
{
    public class AddressVM
    {
        [Required(ErrorMessage = "AddressRequired")]
        public string AddressName { get; set; }

        [ModelBinder(typeof(AddressFloatModelBinder))]
        public float Lng { get; set; }

        [ModelBinder(typeof(AddressFloatModelBinder))] // Применяем кастомный байндер для свойства Lat
        public float Lat { get; set; }
        public int HotelId { get; set; }
    }
}
