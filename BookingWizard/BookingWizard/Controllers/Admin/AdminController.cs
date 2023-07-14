using AutoMapper;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.DAL.Entities;
using BookingWizard.ModelsVM.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.Controllers.Admin
{
    [Authorize(Roles = "Owner")]
    public class AdminController : Controller
    {

        IHotelService _hotelService;
        IMapper _map;

        public AdminController(IHotelService hotelService, IMapper map)
        {

            _hotelService = hotelService;
            _map = map;
        }
        public IActionResult MyHotels(string userId)
        {
            var hotels = _hotelService.GetAll(userId);
            return View(_map.Map<IEnumerable<HotelVM>>(hotels));
        }

        public IActionResult Statistic() 
        {


            return View(); 
        }

    }
}
