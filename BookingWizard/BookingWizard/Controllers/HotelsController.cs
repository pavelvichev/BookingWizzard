using AutoMapper;
using BookingWizard.Core.Entities;
using BookingWizard.Core.Interfaces;
using BookingWizard.Infrastrucure.Data;
using BookingWizard.Infrastrucure.Repositories;
using BookingWizard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookingWizard.Controllers
{
	public class HotelsController : Controller
	{


		IHotelRepository<Hotel> _hotelRepository;
		IHotelRoomRepository<hotelRoom> _hotelRoomRepository;
		IMapper _map;

		public HotelsController(IHotelRepository<Hotel> hotelRepository, IMapper map, IHotelRoomRepository<hotelRoom> hotelRoomRepository)
		{

			_hotelRepository = hotelRepository;
			_hotelRoomRepository = hotelRoomRepository;
			_map = map;
		}
		public IActionResult Hotels()
		{

			IEnumerable<Hotel> hotelList = _hotelRepository.GetAll();
			IEnumerable<HotelDTO> hotelDTOList = _map.Map<IEnumerable<Hotel>, IEnumerable<HotelDTO>>(hotelList);
			return View(hotelDTOList);
		}


		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(HotelDTO hotelDTO)
		{

			if (ModelState.IsValid)
			{

				var hotel = _map.Map<HotelDTO, Hotel>(hotelDTO);
				_hotelRepository.Add(hotel);

			}
			return RedirectToAction("Hotels");
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var hotel = _hotelRepository.Get(id);

			var hotelMod = _map.Map<Hotel, HotelDTO>(hotel);
			return View(hotelMod);
		}

		[HttpPost]
		public IActionResult Edit(HotelDTO hotelDTO)
		{

			if (ModelState.IsValid)
			{
				var hotel = _map.Map<Hotel>(hotelDTO);
				_hotelRepository.Update(hotel);
				return RedirectToAction("Hotels");
			}
			return View();

		}

		public IActionResult Hotel(int id)
		{



			Hotel hotel = _hotelRepository.Get(id);

			hotel.roomList = _hotelRoomRepository.GetAll(id);

			var hotelDTO = _map.Map<HotelDTO>(hotel);
			return View(hotelDTO);


		}
		[HttpPost]
		public IActionResult AddRoom(HotelDTO hotelDTO)
		{

			Hotel hotel = _hotelRepository.Get(hotelDTO.Id);
			


			if (ModelState.IsValid)
			{

				_hotelRoomRepository.Add(_map.Map<hotelRoom>(hotelDTO.room), hotelDTO.Id);
				return RedirectToAction("Hotel", new { id = hotel.Id });
			}

			return View();

		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			Hotel hotel = _hotelRepository.Get(id);
			_hotelRepository.Delete(hotel);

			return RedirectToAction("Hotels");
		}
	}
}
	
