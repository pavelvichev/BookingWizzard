using BookingWizard.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rijndael256;
using System.Data;
using System.Security.Claims;

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
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(vm.UserName);
                
                if (user != null)
                {
                    await Authenticate(user);


                    return Redirect(vm.ReturnUrl);
                }
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
            

            if (ModelState.IsValid)
            {
                var user = new IdentityUser(vm.UserName);
                
                
                    var result = await _userManager.CreateAsync(user, vm.Password);
                        if (result.Succeeded) {
                        await Authenticate(user); // аутентификация
                        return Redirect(vm.ReturnUrl);
                       }
                
     
            }
            return View();
        }

        private async Task Authenticate(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            // создаем один claim

            string role = ""; 
            if(roles.Count > 1)
            {
                 role = roles.FirstOrDefault();
            }
            else
            {
                
                 role = "Guest";
                 await _userManager.AddToRoleAsync(user, role);
            }
            var claims = new List<Claim>
            {
                new Claim("name", user.UserName),
                
                new Claim("role", role)
            };
          

            
            // установка аутентификационных куки
            await _signInManager.SignInWithClaimsAsync(user, true ,claims);
        }
    }
}
