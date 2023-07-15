using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace BookingWizard.Infrastructure
{
    public class FloatModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            try
            {
                var floatValue = float.Parse(value, CultureInfo.InvariantCulture);
                bindingContext.Result = ModelBindingResult.Success(floatValue);
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.TryAddModelError(modelName, ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
