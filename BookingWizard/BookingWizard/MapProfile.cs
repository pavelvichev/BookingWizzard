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
				// Добавьте другие необходимые сопоставления здесь
			}
		}
	
}
