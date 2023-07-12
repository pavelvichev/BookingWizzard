using AutoMapper;
using BookingWizard.BLL.Interfaces.IRooms;
using BookingWizard.ModelsVM.HotelRooms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWizard.Controllers.Rooms
{
    public class PhotoRoomsController : Controller
    {

        IPhotoRoomsService _photoRoomsService;
        IHotelRoomService _hotelRoomService;
        public PhotoRoomsController(IPhotoRoomsService photoRoomsService, IHotelRoomService hotelRoomService)
        {
            _photoRoomsService = photoRoomsService;
            _hotelRoomService = hotelRoomService;
        }

        public ActionResult GetPhoto(int id)
        {
            var photo = _photoRoomsService.GetPhoto(id);
            if (photo != null)
            {
                return File(photo.ImageData, "image/jpeg"); // предполагается, что все фотографии являются JPEG
            }

            return null; // или верните другой результат, если фотография не найдена
        }

        public IActionResult DeletePhoto(int id, int roomId)
        {
            try
            {
                _photoRoomsService.DeletePhoto(id, roomId);
            }
            catch (Exception e)
            {
                TempData["OnePhotoLeft"] = e.Message;
            }
            return Ok();
        }

        public IActionResult PhotoUpload(List<IFormFile> files, string model)
        {
            var modelVM = JsonConvert.DeserializeObject<HotelRoomVM>(model);
            var room = _hotelRoomService.Get(modelVM.Id);

            room.ImageModelList = files;
            _photoRoomsService.PhotoUpload(room);

            // Обновляем блок с фотографиями в представлении Hotel
            room = _hotelRoomService.Get(modelVM.Id); // Получаем обновленный объект hotel с актуальными данными

            var json = JsonConvert.SerializeObject(room.Images);
            return Content(json, "application/json");
        }
    }
}