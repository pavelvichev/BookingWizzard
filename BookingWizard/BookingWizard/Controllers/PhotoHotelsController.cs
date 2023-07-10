using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWizard.Controllers
{
	//All about photo
	public class PhotoHotelsController : Controller
	{

		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;

		IWebHostEnvironment _webHostEnvironment;

		public PhotoHotelsController(IHotelService hotelService, IHotelRoomService hotelRoomService, IWebHostEnvironment webHostEnvironment)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult DeletePhoto(int id, int HotelId)
		{
			_hotelService.DeletePhoto(id);

			return RedirectToAction("Hotel", "Hotels", new
			{
				id = HotelId
			});
		}

		public IActionResult PhotoUpload(List<IFormFile> files, string model)
		{
			var modelVM = JsonConvert.DeserializeObject<HotelVM>(model);
			var hotel = _hotelService.Get(modelVM.Id);

			hotel.ImageModelList = files;
			_hotelService.PhotoUpload(hotel);

			// Обновляем блок с фотографиями в представлении Hotel
			hotel = _hotelService.Get(modelVM.Id); // Получаем обновленный объект hotel с актуальными данными

			var json = JsonConvert.SerializeObject(hotel.Images);
			return Content(json, "application/json");
		}

		public ActionResult GetPhoto(int id)
		{
			var photo = _hotelService.GetPhoto(id);
			if (photo != null)
			{
				return File(photo.ImageData, "image/jpeg"); // предполагается, что все фотографии являются JPEG
			}

			return null; // или верните другой результат, если фотография не найдена
		}

	}

}