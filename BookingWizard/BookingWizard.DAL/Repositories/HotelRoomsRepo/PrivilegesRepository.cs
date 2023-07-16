using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Repositories.HotelRoomsRepo
{
    public class PrivilegesRepository : IPrivilegesRepository
    {
        BookingDbContext _context;
        public PrivilegesRepository(BookingDbContext context)
        {
            _context= context;
        }

        public Privileges Add(Privileges privileges)
        {
            _context.Privileges.Add(privileges);
            _context.SaveChanges(); 
            return privileges;
        }

        public void Delete(int id)
        {
            var item = _context.Privileges.FirstOrDefault(x => x.Id == id);
            _context.Privileges.Remove(item);
            _context.SaveChanges();
        }

        public Privileges Get(int id)
        {
            return _context.Privileges.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Privileges> GetAll(int roomId)
        {
            return _context.Privileges.Where(x => x.HotelRoomId == roomId);
        }

        public Privileges Update(Privileges privileges)
        {
            _context.Privileges.Update(privileges);
            _context.SaveChanges();
            return privileges;
        }
    }
}
