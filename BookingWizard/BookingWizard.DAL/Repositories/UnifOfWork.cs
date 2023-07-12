using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Interfaces.IBookingRepo;
using BookingWizard.DAL.Interfaces.IHotelRepo;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using BookingWizard.DAL.Repositories.BookingRepo;
using BookingWizard.DAL.Repositories.HotelRepo;
using BookingWizard.DAL.Repositories.HotelRoomsRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Repositories
{
    public class UnifOfWork : IUnitOfWork
	{
		BookingDbContext _context;
		HotelRepository _hotelRepository;
		HotelRoomsRepository _roomsRepository;
		BookingRepository _bookingRepository;
        IPhotoHotelsRepository _photoHotelsRepository;
        IPhotoRoomsRepository _photoRoomsRepository;

		private readonly IStringLocalizer<BookingRepository> _bookingLocalizer;
		private readonly IStringLocalizer<HotelRepository> _hotelLocalizer;
		private readonly IStringLocalizer<PhotoHotelsRepository> _photoHotelsLocalizer;
		private readonly IStringLocalizer<PhotoRoomsRepository> _photoRoomsLocalizer;


        public UnifOfWork(BookingDbContext context, IStringLocalizer<BookingRepository> bookingLocalizer, IStringLocalizer<HotelRepository> hotelLocalizer, IPhotoHotelsRepository photoHotelsRepository, IStringLocalizer<PhotoHotelsRepository> photoHotelsLocalizer, IStringLocalizer<PhotoRoomsRepository> photoRoomsLocalizer, IPhotoRoomsRepository photoRoomsRepository)
        {
            _context = context;
            _bookingLocalizer = bookingLocalizer;
            _hotelLocalizer = hotelLocalizer;
            _photoHotelsLocalizer = photoHotelsLocalizer;
            _photoRoomsLocalizer = photoRoomsLocalizer;
            _photoHotelsRepository = photoHotelsRepository;
            _photoRoomsRepository = photoRoomsRepository;
        }

        public IHotelRepository<Hotel> Hotels
		{
			get
			{
				if (_hotelRepository == null)
					_hotelRepository = new HotelRepository(_context, _hotelLocalizer, _photoHotelsRepository);
				return _hotelRepository;
			}
		}
		public IBookingRepository Booking
		{
			get
			{
				if (_bookingRepository == null)
					_bookingRepository = new BookingRepository(_context, _bookingLocalizer);
				return _bookingRepository;
			}
		}

		public IHotelRoomsRepository Rooms
		{
			get
			{
				if (_roomsRepository == null)
					_roomsRepository = new HotelRoomsRepository(_context, _photoRoomsRepository);
				return _roomsRepository;
			}
		}
		public IPhotoHotelsRepository PhotoHotels
		{
			get
			{
				if (_photoHotelsRepository == null)
					_photoHotelsRepository = new PhotoHotelsRepository(_context, _photoHotelsLocalizer);
				return _photoHotelsRepository;
			}
		}

		public IPhotoRoomsRepository PhotoRooms
		{
			get
			{
				if (_photoRoomsRepository == null)
					_photoRoomsRepository = new PhotoRoomsRepository(_context, _photoRoomsLocalizer);
				return _photoRoomsRepository;
			}
		}


    

    }
}
