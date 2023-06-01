using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BookingWizard.BLL.DTO;
using BookingWizard.ModelsVM;

namespace BookingWizard.Controllers
{
	public class HotelsController : Controller
	{


		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IMapper _map;

		public HotelsController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_map = map;
		}
		public IActionResult Hotels()
		{


			IEnumerable<HotelVM> hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());
			return View(hotelVMList);
		}


		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(HotelVM hotelVM)
		{

			if (ModelState.IsValid)
			{

				var hotel = _map.Map<HotelDTO>(hotelVM);
				_hotelService.Add(hotel);
                return RedirectToAction("Hotels");

            }
            return View();
        }
		[HttpGet]
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
				var hotel = _map.Map<HotelDTO>(hotelVM);
				_hotelService.Update(hotel);
				return RedirectToAction("Hotels");
			}
			return View();

		}

		public IActionResult Hotel(int id)
		{

			var hotel = _hotelService.Get(id);

			hotel.roomList = _map.Map<IEnumerable<hotelRoomDTO>>(_hotelRoomService.GetAll(id));

			var hotelDTO = _map.Map<HotelVM>(hotel);
			return View(hotelDTO);


		}
		[HttpPost]
		public IActionResult AddRoom(HotelVM hotelVM)
		{

			var hotel = _hotelService.Get(hotelVM.Id);
			


			if (ModelState.IsValid)
			{

				_hotelRoomService.Add(_map.Map<hotelRoomDTO>(hotelVM.room), hotelVM.Id);
				return RedirectToAction("Hotel", new { id = hotel.Id });
			}

			return View();

		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var hotel = _hotelService.Get(id);
			_hotelService.Delete(hotel);

			return RedirectToAction("Hotels");
		}
	}
}
	
