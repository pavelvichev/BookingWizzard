using BookingWizard.DAL.Entities.HotelRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces.IHotelRoomsRepo
{
    public interface IPrivilegesRepository
    {
        public Privileges Add(Privileges privileges);
        public Privileges Update(Privileges privileges);
        public void Delete(int id);
        public IEnumerable<Privileges> GetAll(int roomId);
        public Privileges Get(int id);
    }
}
