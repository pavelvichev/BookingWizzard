using BookingWizard.DAL.Interfaces;
using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces
{
	public interface IUnitOfWork
	{
		IHotelRepository<Hotel> Hotels { get; }
		IHotelRoomRepository<hotelRoom> Rooms { get; }
		void Save();
	}
}
