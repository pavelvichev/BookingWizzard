using AutoMapper;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.ModelsVM.Hotels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWizard.Controllers.Hotels
{
    //All about photo
    public class PhotoHotelsController : Controller
    {

        IHotelService _hotelService;
        IPhotoHotelsService _photoHotelsService;


        public PhotoHotelsController(IHotelService hotelService, IPhotoHotelsService photoHotelsService)
        {

            _hotelService = hotelService;
            _photoHotelsService = photoHotelsService;
        }
        public IActionResult DeletePhoto(int id, int HotelId)
        {
            try
            {
                _photoHotelsService.DeletePhoto(id, HotelId);
            }
            catch (Exception e)
            {
                TempData["OnePhotoLeft"] = e.Message;
            }
            return Ok();
        }

        public IActionResult PhotoUpload(List<IFormFile> files, string model)
        {
            var modelVM = JsonConvert.DeserializeObject<HotelVM>(model);
            var hotel = _hotelService.Get(modelVM.Id);

            hotel.ImageModelList = files;
            _photoHotelsService.PhotoUpload(hotel);

            // Обновляем блок с фотографиями в представлении Hotel
            hotel = _hotelService.Get(modelVM.Id); // Получаем обновленный объект hotel с актуальными данными

            var json = JsonConvert.SerializeObject(hotel.Images);
            return Content(json, "application/json");
        }

        public ActionResult GetPhoto(int id)
        {
            var photo = _photoHotelsService.GetPhoto(id);
            if (photo != null)
            {
                return File(photo.ImageData, "image/jpeg"); // предполагается, что все фотографии являются JPEG
            }

            return null; // или верните другой результат, если фотография не найдена
        }

    }

}