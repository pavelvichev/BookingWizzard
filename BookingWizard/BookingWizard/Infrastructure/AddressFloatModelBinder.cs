using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace BookingWizard.Infrastructure
{	
		public class AddressFloatModelBinder : IModelBinder
		{
		
			public Task BindModelAsync(ModelBindingContext bindingContext)
			{
				var addressVM = new AddressVM();

				// Получение значений свойств модели AddressVM из контекста привязки
				var addressNameValue = bindingContext.ValueProvider.GetValue("Address.AddressName");
				var latValue = bindingContext.ValueProvider.GetValue("Lat");
				var lngValue = bindingContext.ValueProvider.GetValue("Lng");

				// Привязка значения AddressName
				if (addressNameValue != ValueProviderResult.None)
				{
					addressVM.AddressName = addressNameValue.FirstValue;
				}

				// Привязка значения Lat
				if (latValue != ValueProviderResult.None && float.TryParse(latValue.FirstValue, NumberStyles.Float, CultureInfo.InvariantCulture, out float lat))
				{
					addressVM.Lat = lat;
				}

				// Привязка значения Lng
				if (lngValue != ValueProviderResult.None && float.TryParse(lngValue.FirstValue, NumberStyles.Float, CultureInfo.InvariantCulture, out float lng))
				{
					addressVM.Lng = lng;
				}

				// Установка привязанной модели в контекст привязки
				bindingContext.Result = ModelBindingResult.Success(addressVM);

				return Task.CompletedTask;
			}
		}
}

