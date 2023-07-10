using AutoMapper;
using BookingWizard.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using BookingWizard.DAL.Entities;

namespace BookingWizard.Controllers
{

    public class HotelsController : Controller
	{

		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IMapper _map;
		IWebHostEnvironment _webHostEnvironment;

		public HotelsController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService, IWebHostEnvironment webHostEnvironment)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_webHostEnvironment = webHostEnvironment;
			_map = map;
		}

		public IActionResult Hotels(string Address, float Lat, float Lng)
		{
			IEnumerable<HotelVM> hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());

            try
			{
				 hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.Search(Address, Lat, Lng));
			}
			catch(Exception e)
			{
				TempData["noHotels"] = e.Message;
			}
			foreach (var item in hotelVMList)
			{
				item.Image = item.Images.FirstOrDefault();
			}
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

			hotelMod.Image = hotelMod.Images.FirstOrDefault();
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

		public IActionResult Hotel(int id, string searchString)
		{

			var hotelDTO = _hotelService.Get(id);

			hotelDTO.roomList = _map.Map<IEnumerable<hotelRoom>>(_hotelRoomService.GetAll(id, searchString));

			var hotel = _map.Map<HotelVM>(hotelDTO);
			hotel.Image = hotel.Images.FirstOrDefault();

			foreach (var item in hotel.roomList)
			{
				item.Image = item.Images.FirstOrDefault();
			}

			var selectList = new SelectList(hotelRoomVM.NumbersOfPeople);

			ViewBag.NumberList = selectList;

			return View(hotel);

		}

		[HttpPost]
		public IActionResult AddRoom(HotelVM hotelVM)
		{

			var hotel = _hotelService.Get(hotelVM.Id);

			_hotelRoomService.Add(_map.Map<hotelRoom>(hotelVM.Room), hotelVM.Id);

			return RedirectToAction("Hotel", new
			{
				id = hotel.Id
			});

		}

		public IActionResult Delete(int id)
		{
			var hotel = _hotelService.Get(id);

			_hotelService.Delete(hotel);

			return RedirectToAction("Hotels");
		}

	}
}