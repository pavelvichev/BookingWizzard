﻿using AutoMapper;
using BookingWizard.DAL.Entities;
using BookingWizard.ModelsVM;

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
            CreateMap<hotelRoom, hotelRoomVM>()
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
