using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces
{
	public interface IHotelRoomRepository<T>
	{
		
		public T Add(T item, int hotelId);
		public T Delete(T item);
		public T Update(T item);
		public T Get(int Id);
		IEnumerable<T> GetAll(int hotelId, string searchString = "");
	}
}
