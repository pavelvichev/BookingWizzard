using BookingWizard.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userInManager)
        {
            _signInManager = signInManager;
            _userManager = userInManager;
        }

        //LOGIN


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View (new LoginViewModel {ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }
            else if(result.IsLockedOut) 
            {

            }
            return View(vm);
        }


        //REGISTER



        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View (new RegisterViewModel {ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new IdentityUser(vm.UserName);
            var result =  await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(vm.ReturnUrl);
            }
            
            return View();
        }
    }
}
