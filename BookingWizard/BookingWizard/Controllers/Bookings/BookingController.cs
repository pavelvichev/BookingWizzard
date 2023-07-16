using AutoMapper;
using BookingWizard.BLL.Interfaces.IBookingServices;
using BookingWizard.BLL.Interfaces.IHotelRoomsServices;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using System.Text.Json;

namespace BookingWizard.Controllers.Bookings
{
	public class BookingController : Controller
	{
		IBookingService _bookingService;
		IHotelRoomService _hotelRoomService;
		IMapper _map;
		private readonly IStringLocalizer<BookingController> _localizer;

		public BookingController(
			IMapper map,
			IBookingService bookingService,
			IHotelRoomService hotelRoomService,
			IStringLocalizer<BookingController> localizer
		)
		{
			_bookingService = bookingService;
			_map = map;
			_hotelRoomService = hotelRoomService;
			_localizer = localizer;
		}
		[Authorize]
		public IActionResult Booking(int id)
		{
			var booking = new BookingVM();
			booking.roomId = id;
			booking.RoomName = _hotelRoomService.Get(id).Name;
			booking.HotelName = _hotelRoomService.Get(id).Hotel.HotelName;
			return View(booking);
		}

		[HttpPost]
		public IActionResult Booking(BookingVM booking)
		{
			if (User.Identity.IsAuthenticated)
			{
                if (ModelState.IsValid)
                {
					booking.allPrice = _bookingService.CalcPrice(_map.Map<Booking>(booking));
                    string bookingJson = JsonSerializer.Serialize(booking);
  
					return RedirectToAction("BookingInfo", new { bookingJson = bookingJson });
                }
            }
			else
			{
				TempData["ErrorMessageFromBooking"] = _localizer["notAuthenticated"];

            }
            return RedirectToAction("Room", "Rooms", new { id = booking.roomId });

        }



        public IActionResult BookingInfo(string bookingJson)
		{
			
            var booking  = JsonSerializer.Deserialize<BookingVM>(bookingJson);

            return View(booking);	
		}

		[HttpPost]
        public IActionResult BookingComplete(BookingVM booking)
        {
            try
            {
                _bookingService.Add(_map.Map<Booking>(booking));
                var localizedString = _localizer["MessageFromBooking"];
                var serializedString = localizedString.Value;
                TempData["MessageFromBooking"] = serializedString;

                return RedirectToAction("Room", "Rooms", new { id = booking.roomId });

            }
            catch (Exception ex)
            {
                TempData["ErrorMessageFromBooking"] = ex.Message;
                string bookingJson = JsonSerializer.Serialize(booking);
                
                return RedirectToAction("BookingInfo", new { bookingJson = bookingJson });
            }

          
        }

        public IActionResult AllOrdersByUser()
		{
			var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			return View(_map.Map<IEnumerable<BookingVM>>(_bookingService.GetAll(currentUserId)));
		}
		public IActionResult Delete(int id)
		{
			_bookingService.Delete(id);

			return RedirectToAction("AllOrdersByUser");
		}
	}
}
