using BookingWizard.IdentityServer.Models;
using IdentityServer4;
using IdentityServer4.Services;
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
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userInManager, IIdentityServerInteractionService  interactionService )
        {
            _signInManager = signInManager;
            _userManager = userInManager;
            _interactionService = interactionService;
          
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
                        await Authenticate(user);
                        return Redirect(vm.ReturnUrl);
                       }
                
     
            }
            return View(vm);
        }

        public async Task<IActionResult> LogOut(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutResult =  await _interactionService.GetLogoutContextAsync(logoutId);

            if(string.IsNullOrEmpty(logoutResult.PostLogoutRedirectUri))
                return RedirectToAction("Index", "Home");

            return Redirect(logoutResult.PostLogoutRedirectUri);

        }
        


        private async Task Authenticate(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            // создаем один claim

            string role = ""; 
            if(roles.FirstOrDefault() == "Admin")
            {
                role = "Admin";
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
          

            await _signInManager.SignInWithClaimsAsync(user, true ,claims);
        }
    }
}
