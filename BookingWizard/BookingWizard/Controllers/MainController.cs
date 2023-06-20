using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.Controllers
{
	public class MainController : Controller
	{
		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IBookingService _bookService;
		IMapper _map;

		public MainController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService, IBookingService bookingService)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_map = map;
			_bookService = bookingService;
		}
		public IActionResult Main(string? searchString)
		{

			IEnumerable<HotelVM> hotelVMList;
			if (!string.IsNullOrEmpty(searchString))
			{
				hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll(searchString));
				return View(hotelVMList);
			}

			hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());
			return View(hotelVMList);

		}
		public IActionResult Hotel(int id, string searchstring)
		{

			var hotel = _hotelService.Get(id);

			hotel.roomList = _map.Map<IEnumerable<hotelRoom>>(_hotelRoomService.GetAll(id, searchstring));

			var hotelDTO = _map.Map<HotelVM>(hotel);
			return View(hotelDTO);

		}
		public IActionResult Room(int id, string searchstring)
		{

			var hotelRoom = _map.Map<hotelRoomVM>(_hotelRoomService.Get(id));
			return View(hotelRoom);
		}


		public IActionResult Booking(BookingVM booking)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_bookService.Add(_map.Map<Booking>(booking));
                    uint sum = _bookService.CalcPrice(_map.Map<Booking>(booking));
                    TempData["MessageFromBooking"] = "Booking correctly added";


				}
				catch (Exception ex)
				{
					TempData["ErrorMessageFromBooking"] = ex.Message;

				}	

			}
			return RedirectToAction("Room", new { id = booking.roomId });
		}

	}
}