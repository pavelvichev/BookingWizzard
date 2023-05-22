using AutoMapper;
using BookingWizard.Core.Entities;
using BookingWizard.Core.Interfaces;
using BookingWizard.Infrastrucure.Data;
using BookingWizard.Infrastrucure.Repositories;
using BookingWizard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookingWizard.Controllers
{
    public class HotelsController : Controller
    {
        
        AppDbContext _context;
        IEntityRepository<Hotel> _hotelRepository;
        IMapper _map;

        public HotelsController(AppDbContext context, IEntityRepository<Hotel> hotelRepository,IMapper map)
        {
            _context= context;
            _hotelRepository= hotelRepository;
            _map= map;
        }
		public IActionResult Hotels()
        {
		
			IEnumerable<Hotel> hotelList = _hotelRepository.GetAll();
			IEnumerable<HotelDTO> hotelDTOList = _map.Map<IEnumerable<Hotel>,IEnumerable<HotelDTO>>(hotelList);
			return View(hotelDTOList);
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(HotelDTO hotelDTO)
        {

            if (ModelState.IsValid)
            {
				
                var hotel = _map.Map<HotelDTO,Hotel>(hotelDTO);
				_hotelRepository.Add(hotel);
                
            }
            return   RedirectToAction("Hotels");
        }
  //      [HttpGet]
  //      public IActionResult Edit(int id)
  //      {
  //          HotelDTO hotel = _context.hotels.FirstOrDefault(x => x.Id == id);
  //          hotel.address = _context.Address.FirstOrDefault(x => hotel.addressId == x.Id);
           
  //          return View(hotel);
  //      }

  //      [HttpPost]
  //      public IActionResult Edit(HotelDTO hotel)
  //      {

  //          if (ModelState.IsValid)
  //          {
                
  //              _context.Update(hotel);
  //              _context.SaveChanges();
  //          }
  //          return RedirectToAction("Hotels");
  //      }

  //      public IActionResult Hotel(int id)
  //      {
        
  //          if (ModelState.IsValid)
  //          {
               
  //              HotelDTO hotel = _context.hotels.FirstOrDefault(x => x.Id == id);
  //              hotel.address = _context.Address.FirstOrDefault(x => hotel.addressId == x.Id);
  //              hotel.room = new hotelRoomDTO();
  //              hotel.room.Hotel = hotel;
  //              hotel.roomList = _context.hotelRooms.Where(x => x.HotelId == hotel.Id).ToList();
				

		//		return View(hotel);
  //          }
  //        return View();
		//}
  //      [HttpPost]
  //      public IActionResult AddRoom(HotelDTO hotelCurr)
  //      {
  //          HotelDTO hotel = _context.hotels.FirstOrDefault(x => x.Id == hotelCurr.Id);
         
  //          hotelCurr.room.Hotel = hotel;
  //          hotelCurr.room.HotelId = hotel.Id;
  //          hotel.room = hotelCurr.room;
           
           
            
  //          if (ModelState.IsValid)
  //          {

  //              _context.hotelRooms.Add(hotel.room);
  //              _context.SaveChanges();
				
		//	}
			
		//	return RedirectToAction("Hotel", new { id = hotel.Id });
  //      }

  //      [HttpPost]
  //      public IActionResult Delete(int id)
  //      {
  //          HotelDTO hotel = _context.hotels.FirstOrDefault(x => x.Id == id);
  //          _context.Remove(hotel);
  //          _context.SaveChanges();
		//	return RedirectToAction("Hotels");
		//}
    }
}
