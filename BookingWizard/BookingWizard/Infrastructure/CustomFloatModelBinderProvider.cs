using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using BookingWizard.ModelsVM.Hotels;

namespace BookingWizard.Infrastructure
{
    public class CustomDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {

            IModelBinder binder;
            if (context.Metadata.ModelType == typeof(AddressVM))
            {
                binder = new AddressFloatModelBinder();
                return binder;
            }
            else if (context.Metadata.ModelType == typeof(float))
            {
                binder = new FloatModelBinder();
                return binder;
            }
            return null;
        }
    }
}

