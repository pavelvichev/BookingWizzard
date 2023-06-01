using AutoMapper;
using BookingWizard.BLL.DTO;
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
		IMapper _map;

		public MainController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_map = map;
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

			hotel.roomList = _map.Map<IEnumerable<hotelRoomDTO>>(_hotelRoomService.GetAll(id, searchstring));

			var hotelDTO = _map.Map<HotelVM>(hotel);
			return View(hotelDTO);

		}
		public IActionResult Room(int id, string searchstring)
		{

			var hotelRoom = _map.Map<hotelRoomVM>(_hotelRoomService.Get(id));
			return View(hotelRoom);
		}

		

	}
}
