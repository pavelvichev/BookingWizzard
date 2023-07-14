using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.ModelsVM.Hotels;
using BookingWizard.ModelsVM.HotelRooms;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.BLL.Interfaces.IHotelRoomsServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BookingWizard.Controllers.Hotels
{

    public class HotelsController : Controller
    {

        IHotelService _hotelService;
        IHotelRoomService _hotelRoomService;
        IReviewsService _reviewsService;
        IMapper _map;
        IWebHostEnvironment _webHostEnvironment;
		


		public HotelsController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService, IWebHostEnvironment webHostEnvironment, IReviewsService reviewsService)
        {

            _hotelService = hotelService;
            _hotelRoomService = hotelRoomService;
            _webHostEnvironment = webHostEnvironment;
            _reviewsService = reviewsService;
            _map = map;
          
        }
        [Route("/")]
        public IActionResult Hotels(string Address, float Lat, float Lng)
        {
            IEnumerable<HotelVM> hotelVMList = null;

            try
            {
                hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.Search(Address, Lat, Lng));
            }
            catch (Exception e)
            {
                TempData["noHotels"] = e.Message;
            }


            hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());

            return View(hotelVMList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var Hotel = new HotelVM();
            return View(Hotel);
        }

        [HttpPost]
        public IActionResult Add(HotelVM hotelVM)
        {
            if (ModelState.IsValid)
            {
                var hotel = _map.Map<Hotel>(hotelVM);
                _hotelService.Add(hotel);

                return RedirectToAction("Hotels");
            }
            return View(hotelVM);
        }

        public IActionResult Edit(int id)
        {
            var hotel = _hotelService.Get(id);
            var hotelMod = _map.Map<HotelVM>(hotel);
            hotelMod.Image = hotelMod.Images.FirstOrDefault();
            hotelMod.ImageModelList = null;
            return View(hotelMod);
        }

        [HttpPost]
        public IActionResult Edit(HotelVM hotelVM)
        {
			ModelState.Remove("ImageModelList");
			if (ModelState.IsValid)
            {
                var hotel = _map.Map<Hotel>(hotelVM);
                _hotelService.Update(hotel);
                return RedirectToAction("Hotels");
            }
            return View(hotelVM);

        }

        public IActionResult Hotel(int id, int NumberOfPeople)
        {

            var hotelDTO = _hotelService.Get(id);

            hotelDTO.roomList = _map.Map<IEnumerable<HotelRoom>>(_hotelRoomService.GetAll(id, NumberOfPeople));

            var hotel = _map.Map<HotelVM>(hotelDTO);

            hotel.ReviewVM = new ReviewVM();
            hotel.ReviewVM.User = new IdentityUser();
            hotel.AllReviews = _map.Map<IEnumerable<ReviewVM>>(_reviewsService.GetAll(id));

            return View(hotel);

        }

        

        public IActionResult Delete(int id)
        {
            var hotel = _hotelService.Get(id);

            _hotelService.Delete(hotel);

            return RedirectToAction("Hotels");
        }

    }
}