using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces.IBookingRepo;
using BookingWizard.DAL.Interfaces.IHotelRepo;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using BookingWizard.DAL.Interfaces.IUsersRepo;
using Microsoft.Extensions.Localization;


namespace BookingWizard.DAL.Interfaces
{
    public interface IUnitOfWork
	{
		IHotelRepository<Hotel> Hotels { get; }
		IHotelRoomsRepository Rooms { get; }
		IBookingRepository Booking { get; }
		IPhotoHotelsRepository PhotoHotels { get; }
		IPhotoRoomsRepository PhotoRooms { get; }
		IReviewsRepository Reviews { get; }
		IUsersRepository Users { get; }
	
		
	}
}
