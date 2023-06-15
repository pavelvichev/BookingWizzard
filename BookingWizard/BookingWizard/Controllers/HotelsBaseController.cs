using AutoMapper;
using BookingWizard.BLL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BookingWizard.BLL.DTO;
using BookingWizard.ModelsVM;


namespace BookingWizard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsBaseController : ControllerBase
	{


		IHotelService _hotelService;
		IHotelRoomService _hotelRoomService;
		IMapper _map;

		public HotelsBaseController(IHotelService hotelService, IMapper map, IHotelRoomService hotelRoomService)
		{

			_hotelService = hotelService;
			_hotelRoomService = hotelRoomService;
			_map = map;
		}

		[HttpGet]
		public IEnumerable<HotelVM> Hotels()
		{


			IEnumerable<HotelVM> hotelVMList = _map.Map<IEnumerable<HotelVM>>(_hotelService.GetAll());
			return hotelVMList;
		}



		[HttpPost]
		public HotelDTO Add([FromBody] HotelVM hotelVM)
		{

			var hotel = _map.Map<HotelDTO>(hotelVM);
			return hotel;
		}


		[HttpPost]
		public HotelDTO Edit([FromBody]HotelVM hotelVM)
		{

			var hotel = _map.Map<HotelDTO>(hotelVM);
			return hotel;


		}
        [HttpGet("{id}", Name = "Hotel")]
        public HotelVM Hotel(int id)
		{

			var hotel = _hotelService.Get(id);

			hotel.roomList = _map.Map<IEnumerable<hotelRoomDTO>>(_hotelRoomService.GetAll(id));

			var hotelDTO = _map.Map<HotelVM>(hotel);
			return hotelDTO;


		}
        [HttpPost]
        public hotelRoomVM AddRoom([FromBody]HotelVM hotelVM)
		{

			return hotelVM.room;

		}

		[HttpDelete("{id}")]
		public HotelDTO Delete(int id)
		{
			var hotel = _hotelService.Get(id);
			

			return hotel;
		}
	}
}
	
