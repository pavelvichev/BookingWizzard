using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.BLL.Services;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.Controllers
{
	public class RoomsController : Controller
	{

		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IBookingService _bookingService;
		IWebHostEnvironment _webHostEnvironment;
		IMapper _map;

		public RoomsController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService, IBookingService bookingService, IWebHostEnvironment webHostEnvironment)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_bookingService = bookingService;
            _webHostEnvironment = webHostEnvironment;
            _map = map;
		}
		public IActionResult Room(int id)
		{
			var curUser = HttpContext.User;
			var hotelRoom = _map.Map<hotelRoomVM>(_hotelRoomService.Get(id));
			return View(hotelRoom);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			
                var room = _hotelRoomService.Get(id);
			var roomMod = _map.Map<hotelRoomVM>(room);
			return View(roomMod);
		}

		[HttpPost]
		public IActionResult Edit(hotelRoomVM roomVM)
		{
          
            if (ModelState.IsValid)
			{
				var room = _map.Map<hotelRoom>(roomVM);
				_hotelRoomService.Update(room);
				return RedirectToAction("Hotel", "Hotels", new { id = room.HotelId });
			}
			return View();

		}


		[HttpPost]
		public IActionResult Delete(int id)
		{
			var room = _map.Map<hotelRoom>(_hotelRoomService.Get(id));


			

            _hotelRoomService.Delete(room);
            return RedirectToAction("Hotel", "Hotels", new { id = room.HotelId });
		}

		public IActionResult Booking(BookingVM booking)
		{
			if (User.Identity.IsAuthenticated)
			{
				if (ModelState.IsValid)
				{
					try
					{
						booking.allPrice = _bookingService.CalcPrice(_map.Map<Booking>(booking));
						_bookingService.Add(_map.Map<Booking>(booking));
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
			return RedirectToAction("Room", "Rooms", new { id = booking.roomId });

		}


       
    }
}
