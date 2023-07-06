using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.BLL.Services;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
	
			var hotelRoom = _map.Map<hotelRoomVM>(_hotelRoomService.Get(id));
			hotelRoom.Image = hotelRoom.Images.FirstOrDefault();
            
            return View(hotelRoom);
		}
		
		public IActionResult Rooms(int id)
		{
	
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


        public IActionResult DeletePhoto(int id, int RoomId)
        {
            _hotelRoomService.DeletePhoto(id);

            return RedirectToAction("Room", new { id = RoomId });
        }

        public IActionResult PhotoUpload(List<IFormFile> files, string model)
        {
            var modelVM = JsonConvert.DeserializeObject<hotelRoomVM>(model);
            var room = _hotelRoomService.Get(modelVM.Id);

            room.ImageModelList = files;
            _hotelRoomService.PhotoUpload(room);

            // Обновляем блок с фотографиями в представлении Hotel
            room = _hotelRoomService.Get(modelVM.Id); // Получаем обновленный объект hotel с актуальными данными

            var json = JsonConvert.SerializeObject(room.Images);
            return Content(json, "application/json");
        }


    }
}
