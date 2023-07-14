using AutoMapper;
using BookingWizard.BLL.Interfaces.IBookingServices;
using BookingWizard.BLL.Interfaces.IHotelRoomsServices;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.BLL.Services;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Interfaces;
using BookingWizard.ModelsVM.HotelRooms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookingWizard.Controllers.Rooms
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

            var hotelRoom = _map.Map<HotelRoomVM>(_hotelRoomService.Get(id));
            hotelRoom.Image = hotelRoom.Images.FirstOrDefault();

            return View(hotelRoom);
        }

        public IActionResult Rooms(int id)
        {

            var hotelRoom = _map.Map<HotelRoomVM>(_hotelRoomService.Get(id));

            return View(hotelRoom);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var room = _hotelRoomService.Get(id);
            var roomMod = _map.Map<HotelRoomVM>(room);
            return View(roomMod);
        }

        [HttpPost]
        public IActionResult Edit(HotelRoomVM roomVM)
        {

            if (ModelState.IsValid)
            {
                var room = _map.Map<HotelRoom>(roomVM);
                _hotelRoomService.Update(room);
                return RedirectToAction("Hotel", "Hotels", new
                {
                    id = room.HotelId
                });
            }
            return View();

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var room = _map.Map<HotelRoom>(_hotelRoomService.Get(id));

            _hotelRoomService.Delete(room);
            return RedirectToAction("Hotel", "Hotels", new
            {
                id = room.HotelId
            });
        }

    }
}