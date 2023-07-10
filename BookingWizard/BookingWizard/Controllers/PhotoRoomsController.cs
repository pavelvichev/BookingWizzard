using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWizard.Controllers
{
	public class PhotoRoomsController : Controller
	{

		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IBookingService _bookingService;
		IWebHostEnvironment _webHostEnvironment;

		public PhotoRoomsController(IHotelService hotelService, IHotelRoomService hotelRoomService, IBookingService bookingService, IWebHostEnvironment webHostEnvironment)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_bookingService = bookingService;
			_webHostEnvironment = webHostEnvironment;

		}

		public ActionResult GetPhoto(int id)
		{
			var photo = _hotelRoomService.GetPhoto(id);
			if (photo != null)
			{
				return File(photo.ImageData, "image/jpeg"); // предполагается, что все фотографии являются JPEG
			}

			return null; // или верните другой результат, если фотография не найдена
		}

		public IActionResult DeletePhoto(int id, int RoomId)
		{
			_hotelRoomService.DeletePhoto(id);

			return RedirectToAction("Room", "Rooms", new
			{
				id = RoomId
			});
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