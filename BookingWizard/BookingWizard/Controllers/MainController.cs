using AutoMapper;
using BookingWizard.BLL.DTO;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using BookingWizard.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.Controllers
{
	public class MainController : Controller
	{
		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IMapper _map;

		public MainController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_map = map;
		}
		public IActionResult Main()
		{


			IEnumerable<HotelVM> hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());
			return View(hotelVMList);
			
		}
		public IActionResult Hotel(int id)
		{

			var hotel = _hotelService.Get(id);

			hotel.roomList = _map.Map<IEnumerable<hotelRoomDTO>>(_hotelRoomService.GetAll(id));

			var hotelDTO = _map.Map<HotelVM>(hotel);
			return View(hotelDTO);

		}
		public IActionResult Room(int id)
		{

			var hotelRoom = _map.Map<hotelRoomDTO>(_hotelRoomService.Get(id));
			return View(hotelRoom);
		}

	}
}
