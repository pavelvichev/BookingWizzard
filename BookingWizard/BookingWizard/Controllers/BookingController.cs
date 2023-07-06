using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.ModelsVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingWizard.Controllers
{
    public class BookingController:Controller
    {


        IBookingService _bookingService;
        IHotelRoomService _hotelRoomService;
        IMapper _map;
        

        public BookingController(IMapper map, IBookingService bookingService, IHotelRoomService hotelRoomService)
        {
            _bookingService = bookingService;
            _map = map;
            _hotelRoomService = hotelRoomService;
            
        }
        [Authorize]
        public IActionResult Booking(int id)
        {
            var booking = new BookingVM();
            booking.roomId = id;
            booking.RoomName = _hotelRoomService.Get(id).Name;
            booking.HotelName = _hotelRoomService.Get(id).Hotel.HotelName;
            return View(booking);
        }
       
        [HttpPost]
        public IActionResult Booking(BookingVM booking)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        booking.Id = 0;
                        booking.allPrice = _bookingService.CalcPrice(_map.Map<Booking>(booking));
                        _bookingService.Add(_map.Map<Booking>(booking));
                        TempData["MessageFromBooking"] = "Booking correctly added";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessageFromBooking"] = ex.Message;
                        return View(booking);
                    }
                }
            }
            else
            {
                TempData["ErrorMessageFromBooking"] = "User do not authenticated";
            }

            return RedirectToAction("Room", "Rooms", new { id = booking.roomId });

        }

        public IActionResult AllOrdersByUser()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
            return View(_map.Map<IEnumerable<BookingVM>>(_bookingService.GetAll(currentUserId)));
        }
        public IActionResult Delete(int id)
        {
            _bookingService.Delete(id);

            return RedirectToAction("AllOrdersByUser");
        }
    }
}
