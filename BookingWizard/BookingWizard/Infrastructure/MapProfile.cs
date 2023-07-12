using AutoMapper;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.ModelsVM;
using BookingWizard.ModelsVM.HotelRooms;
using BookingWizard.ModelsVM.Hotels;

namespace BookingWizard.Infrastructure
{

    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Hotel, HotelVM>()
            .ReverseMap();
            CreateMap<Address, AddressVM>()
            .ReverseMap();
            CreateMap<HotelRoom, HotelRoomVM>()
            .ReverseMap();
            CreateMap<Booking, BookingVM>()
            .ReverseMap();
            CreateMap<HotelImages, HotelImagesVM>()
            .ReverseMap();
            CreateMap<RoomImages, RoomImagesVM>()
            .ReverseMap();
      




        }
    }

}
