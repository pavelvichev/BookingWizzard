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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingWizard.Controllers
{
	[Authorize]
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


		public IActionResult Hotels(string searchString)
		{

			IEnumerable<HotelVM> hotelVMList;
			if (!string.IsNullOrEmpty(searchString))
			{
				hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll(searchString));
				return View(hotelVMList);
			}

			hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());

			foreach(var item in hotelVMList)
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
			var hotelRoom = new hotelRoomVM();
			var selectList = new SelectList(hotelRoom.NumbersOfPeople);

			ViewBag.NumberList = selectList;


			return View(hotel);

		}

		[HttpPost]
		public IActionResult AddRoom(HotelVM hotelVM)
		{
			
			var hotel = _hotelService.Get(hotelVM.Id);
			

			_hotelRoomService.Add(_map.Map<hotelRoom>(hotelVM.room), hotelVM.Id);

			return RedirectToAction("Hotel", new { id = hotel.Id });

		}

		public IActionResult Delete(int id)
		{
			var hotel = _hotelService.Get(id);
			
			_hotelService.Delete(hotel);

			return RedirectToAction("Hotels");
		}

    }
}

