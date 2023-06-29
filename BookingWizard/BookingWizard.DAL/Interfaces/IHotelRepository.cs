using BookingWizard.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces
{
	public interface IHotelRepository<T>
	{
		
		public T Add(T item);
		public T Delete(T item);
		public T Update(T item);
		public T Get(int id);
		IEnumerable<T> GetAll(string name = "");
		public void PhotoUpload(Hotel hotel, int id = 0);
		
		
	}
}
