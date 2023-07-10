using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Entities;
using Microsoft.Extensions.Localization;


namespace BookingWizard.DAL.Interfaces
{
    public interface IUnitOfWork
	{
		IHotelRepository<Hotel> Hotels { get; }
		IHotelRoomRepository<hotelRoom> Rooms { get; }
		IBookingRepository Booking { get; }
	
		
	}
}
