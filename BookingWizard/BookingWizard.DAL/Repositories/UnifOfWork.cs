using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;

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
		hotelRoomsRepository _roomsRepository;
		BookingRepository _bookingRepository;

		private readonly IStringLocalizer<BookingRepository> _bookingLocalizer;
		private readonly IStringLocalizer<HotelRepository> _hotelLocalizer;

		public UnifOfWork(BookingDbContext context, IStringLocalizer<BookingRepository> bookingLocalizer, IStringLocalizer<HotelRepository> hotelLocalizer)
		{
			_context = context;
			_bookingLocalizer = bookingLocalizer;
			_hotelLocalizer= hotelLocalizer;
		}
		public IHotelRepository<Hotel> Hotels
		{
			get
			{
				if (_hotelRepository == null)
					_hotelRepository = new HotelRepository(_context, _hotelLocalizer);
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

		public IHotelRoomRepository<hotelRoom> Rooms
		{
			get
			{
				if (_roomsRepository == null)
					_roomsRepository = new hotelRoomsRepository(_context);
				return _roomsRepository;
			}
		}
    

    }
}
