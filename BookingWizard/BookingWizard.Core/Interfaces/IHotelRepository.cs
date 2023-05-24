using BookingWizard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Core.Interfaces
{
	public interface IHotelRepository<T>
	{
		
		public T Add(T item);
		public T Delete(T item);
		public T Update(T item);
		public T Get(int id);
		IEnumerable<T> GetAll();
	}
}
