using BookingWizard.DAL.Data;
using BookingWizard.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingWizard.DAL.Repositories
{

	public class BookingRepository : IBookingRepository
	{
		readonly BookingDbContext _context;
	


		public BookingRepository(BookingDbContext context)
		{
			_context = context;
		}

		public Entities.Booking Add(Entities.Booking order)
		{
			foreach (var item in _context.Booking.Where(x => x.RoomId == order.RoomId))
			{
                // Проверка вложенного бронирования
                if (order.ArrivalDate <= item.ArrivalDate && order.DateOfDeparture >= item.DateOfDeparture)
                {

                    throw new Exception("Выбранный период полностью или частично перекрывает существующее бронирование");
                }
                if ((order.ArrivalDate >= item.ArrivalDate && order.ArrivalDate < item.DateOfDeparture) ||
					 (order.DateOfDeparture > item.ArrivalDate && order.DateOfDeparture <= item.DateOfDeparture))
                {
                  throw new Exception("Комната уже забронирована в выбранный период");
                }


            }
			var room = _context.hotelRooms.FirstOrDefault(room => room.Id == order.RoomId);
			room.isBooking = true;
			order.Room = null;
			_context.Booking.Add(order);
			_context.SaveChanges();
			return order;
		}

		public Entities.Booking Delete(int id)
		{
			var item = _context.Booking.FirstOrDefault(x => x.Id == id);
			_context.Booking.Remove(item);
			_context.SaveChanges();
			return item;
		}

		public Entities.Booking Get(int id)
		{
			var item = _context.Booking.FirstOrDefault(x => x.Id == id);
			return item;
		}

		public IEnumerable<Entities.Booking> GetAll(string currentUserId)
		{
			var items = _context.Booking.Where(x => x.IdentityUserId == currentUserId).ToList();
			return items;
		}

		public Entities.Booking Update(Entities.Booking item)
		{
			_context.Booking.Update(item);
			return item;
		}
	}
}
