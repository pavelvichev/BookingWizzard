using AutoMapper;
using BookingWizard.BLL.DTO;
using BookingWizard.DAL.Entities;
using BookingWizard.Models;

namespace BookingWizard
{
	
		public class MapProfile : Profile
		{
			public MapProfile()
			{
				CreateMap<Hotel, HotelVM>().ReverseMap();
				CreateMap<HotelVM, HotelDTO>().ReverseMap();
				CreateMap<Address, AddressVM>().ReverseMap();
				CreateMap<hotelRoom, hotelRoomVM>().ReverseMap();
				CreateMap<hotelRoomVM, hotelRoomDTO>().ReverseMap();
				CreateMap<hotelRoom, hotelRoomDTO>().ReverseMap();
				CreateMap<Hotel, HotelDTO>().ReverseMap();
				CreateMap<Address,AddressDTO>().ReverseMap();
				CreateMap<AddressVM,AddressDTO>().ReverseMap();
				// Добавьте другие необходимые сопоставления здесь
			}
		}
	
}
