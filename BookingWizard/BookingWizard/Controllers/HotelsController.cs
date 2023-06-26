using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace BookingWizard.Controllers
{
   
    public class HotelsController : Controller
	{


		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IBookingService _bookingService;
		IMapper _map;

		public HotelsController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService, IBookingService bookingService)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_bookingService = bookingService;
			_map = map;
		}



		
		public IActionResult Search(string searchString, string actionName)
		{
		  TempData["Search"] = searchString;

			return RedirectToAction(actionName);
		}

		[Authorize]
        [HttpGet]
        public IActionResult Hotels()
		{

			string searchString = (string)TempData["Search"];
            IEnumerable<HotelVM> hotelVMList;
            if (!string.IsNullOrEmpty(searchString))
            {
                hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll(searchString));
                return View(hotelVMList);
            }

            hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());
            return View(hotelVMList);
        }

       
        [HttpGet]
		public IActionResult Add()
		{
			return View();
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
            return View();
        }
		
		public IActionResult Edit(int id)
		{
			var hotel = _hotelService.Get(id);

			var hotelMod = _map.Map<HotelVM>(hotel);
			return View(hotelMod);
		}

		[HttpPost]
		public IActionResult Edit(HotelVM hotelVM)
		{

			if (ModelState.IsValid)
			{
				var hotel = _map.Map<Hotel>(hotelVM);
				_hotelService.Update(hotel);
				return RedirectToAction("Hotels");
			}
			return View();

		}
	
		public IActionResult Hotel(int id)
		{
			string searchString = (string)TempData["Search"];
            var hotel = _hotelService.Get(id);

            hotel.roomList = _map.Map<IEnumerable<hotelRoom>>(_hotelRoomService.GetAll(id, searchString));

            var hotelDTO = _map.Map<HotelVM>(hotel);
            return View(hotelDTO);


        }
		[HttpPost]
		public IActionResult AddRoom(HotelVM hotelVM)
		{

			var hotel = _hotelService.Get(hotelVM.Id);
			


			if (ModelState.IsValid)
			{

				_hotelRoomService.Add(_map.Map<hotelRoom>(hotelVM.room), hotelVM.Id);
				return RedirectToAction("Hotel", new { id = hotel.Id });
			}

            return RedirectToAction("Hotel", new { id = hotel.Id });

        }
		
		public IActionResult Delete(int id)
		{
			var hotel = _hotelService.Get(id);
			_hotelService.Delete(hotel);

			return RedirectToAction("Hotels");
		}


        public IActionResult Booking(BookingVM booking)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _bookingService.Add(_map.Map<Booking>(booking));
                        uint sum = _bookingService.CalcPrice(_map.Map<Booking>(booking));
                        TempData["MessageFromBooking"] = "Booking correctly added";


                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessageFromBooking"] = ex.Message;

                    }

                }


            }
            else
            {
                TempData["ErrorMessageFromBooking"] = "User do not authenticated";

            }
            return RedirectToAction("Room","Rooms", new { id = booking.roomId });

        }
    }
}
	
