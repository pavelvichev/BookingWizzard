using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Migrations;
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
		IWebHostEnvironment _webHostEnvironment;

		public UnifOfWork(BookingDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context= context;
			_webHostEnvironment= webHostEnvironment;
		}
		public IHotelRepository<Hotel> Hotels
		{
			get
			{
				if (_hotelRepository == null)
					_hotelRepository = new HotelRepository(_context, _webHostEnvironment);
				return _hotelRepository;
			}
		}
		public IBookingRepository Booking
		{
			get
			{
				if (_bookingRepository == null)
					_bookingRepository = new BookingRepository(_context);
				return _bookingRepository;
			}
		}

		public IHotelRoomRepository<hotelRoom> Rooms
		{
			get
			{
				if (_roomsRepository == null)
					_roomsRepository = new hotelRoomsRepository(_context, _webHostEnvironment);
				return _roomsRepository;
			}
		}

		
	}
}
