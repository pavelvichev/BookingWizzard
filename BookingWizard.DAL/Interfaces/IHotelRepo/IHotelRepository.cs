using BookingWizard.DAL.Entities.Hotels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces.IHotelRepo
{
    public interface IHotelRepository<T>
    {

        public T Add(T item);
        public void Delete(int id);
        public T Update(T item);
        public T Get(int id);
        IEnumerable<T> GetAll(string userId = "");
        public IEnumerable<Hotel> Search(string Address, float Lat, float Lng);



    }
}
