using AutoMapper;
using BookingWizard.BLL.Interfaces.IBookingServices;
using BookingWizard.BLL.Interfaces.IHotelRoomsServices;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.BLL.Services;
using BookingWizard.Controllers.Hotels;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Interfaces;
using BookingWizard.ModelsVM.HotelRooms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace BookingWizard.Controllers.Rooms
{
    public class RoomsController : Controller
    {

        IHotelService _hotelService;
        IHotelRoomService _hotelRoomService;
        IBookingService _bookingService;
      
        IMapper _map;
		IStringLocalizer<RoomsController> _localizer;

		public RoomsController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService, IBookingService bookingService, IStringLocalizer<RoomsController> localizer)
        {

            _hotelService = hotelService;
            _hotelRoomService = hotelRoomService;
            _localizer= localizer;
            _bookingService = bookingService;
            _map = map;
        }
        public IActionResult Room(int id)
        {

            var hotelRoom = _map.Map<HotelRoomVM>(_hotelRoomService.Get(id));
            hotelRoom.Image = hotelRoom.Images.FirstOrDefault();

            return View(hotelRoom);
        }

		[HttpPost]
		public IActionResult AddRoom(HotelRoomVM roomVM)
		{
			ModelState.Remove("PrivilegesList");
			if (ModelState.IsValid)
			{
				_hotelRoomService.Add(_map.Map<HotelRoom>(roomVM), roomVM.HotelId);
			}
			else
			{
				var localizedString = _localizer["ErrorAddRoom"];
				var serializedString = localizedString.Value;
				TempData["ErrorAddRoom"] = serializedString;
			}
			return RedirectToAction("Hotel", "Hotels" , new
			{
				id = roomVM.HotelId
			});

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
			ModelState.Remove("ImageModelList");
			ModelState.Remove("PrivilegesList");
			if (ModelState.IsValid)
            {
                var room = _map.Map<HotelRoom>(roomVM);
                _hotelRoomService.Update(room);
                return RedirectToAction("Hotel", "Hotels", new
                {
                    id = room.HotelId
                });
            }
            return View(roomVM);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var room = _map.Map<HotelRoom>(_hotelRoomService.Get(id));

            _hotelRoomService.Delete(id);
            return RedirectToAction("Hotel", "Hotels", new
            {
                id = room.HotelId
            });
        }

    }
}