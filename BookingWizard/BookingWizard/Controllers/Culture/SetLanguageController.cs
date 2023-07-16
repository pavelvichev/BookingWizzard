using Azure;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.Controllers.Culture
{
    public class SetLanguageController : Controller
    {
        [HttpPost]

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Headers.Add("Cache-Control", "no-cache, no-store");

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );


            return Redirect(returnUrl);
        }
    }
}
