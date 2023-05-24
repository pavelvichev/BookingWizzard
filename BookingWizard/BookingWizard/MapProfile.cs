using AutoMapper;
using BookingWizard.Core.Entities;
using BookingWizard.Models;

namespace BookingWizard
{
	
		public class MapProfile : Profile
		{
			public MapProfile()
			{
				CreateMap<Hotel, HotelDTO>();
				CreateMap<HotelDTO, Hotel>();
				CreateMap<Address, AddressDTO>();
				CreateMap<AddressDTO, Address>();
				CreateMap<hotelRoom, hotelRoomDTO>();
				CreateMap<hotelRoomDTO, hotelRoom>();
				// Добавьте другие необходимые сопоставления здесь
			}
		}
	
}
