using BookingWizard.DAL.Entities.HotelRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces.IHotelRoomsRepo
{
    public interface IHotelRoomsRepository
    {

        public HotelRoom Add(HotelRoom item, int hotelId);

        public HotelRoom Delete(int id);
        public HotelRoom Update(HotelRoom item);
        public HotelRoom Get(int Id);
        IEnumerable<HotelRoom> GetAll(int hotelId, int NumberOfPeople = 0);

    }
}
