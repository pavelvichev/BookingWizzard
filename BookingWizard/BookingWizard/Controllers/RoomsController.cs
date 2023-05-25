using AutoMapper;
using BookingWizard.Core.Entities;
using BookingWizard.Core.Interfaces;
using BookingWizard.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.Controllers
{
	public class RoomsController : Controller
	{

		IHotelRepository<Hotel> _hotelRepository;
		IHotelRoomRepository<hotelRoom> _hotelRoomRepository;
		IMapper _map;

		public RoomsController(IHotelRepository<Hotel> hotelRepository, IMapper map, IHotelRoomRepository<hotelRoom> hotelRoomRepository)
		{

			_hotelRepository = hotelRepository;
			_hotelRoomRepository = hotelRoomRepository;
			_map = map;
		}
		public IActionResult Room(int id)
		{

			var hotelRoom = _map.Map<hotelRoom, hotelRoomDTO>(_hotelRoomRepository.Get(id));
			return View(hotelRoom);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			
			var room = _hotelRoomRepository.Get(id);
			var roomMod = _map.Map<hotelRoom, hotelRoomDTO>(room);
			return View(roomMod);
		}

		[HttpPost]
		public IActionResult Edit(hotelRoomDTO roomDTO)
		{

			if (ModelState.IsValid)
			{
				var room = _map.Map<hotelRoom>(roomDTO);
				_hotelRoomRepository.Update(room);
				return RedirectToAction("Hotel", "Hotels", new { id = room.HotelId });
			}
			return View();
			
		}


		[HttpPost]
		public IActionResult Delete(int id)
		{
			hotelRoom room = _hotelRoomRepository.Get(id);
			_hotelRoomRepository.Delete(room);

			return RedirectToAction("Hotel", "Hotels", new { id = room.HotelId });
		}
	}
}
