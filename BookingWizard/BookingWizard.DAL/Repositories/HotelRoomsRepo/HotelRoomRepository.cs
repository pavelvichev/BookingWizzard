using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Entities.HotelRooms;

namespace BookingWizard.DAL.Repositories.HotelRoomsRepo
{
    public class HotelRoomRepository : IHotelRoomsRepository
    {

        readonly BookingDbContext _context;
        readonly IPhotoRoomsRepository _photoRoomsRepository;
        readonly IPrivilegesRepository _previlegesRepository;

        public HotelRoomRepository(BookingDbContext context, IPhotoRoomsRepository photoRoomsRepository, IPrivilegesRepository previlegesRepository)
        {
            _context = context;
            _photoRoomsRepository = photoRoomsRepository;
            _previlegesRepository = previlegesRepository;
        }
        public HotelRoom Add(HotelRoom item, int hotelId)
        {

            item.HotelId = hotelId;

            _context.HotelRooms.Add(item);
            _context.SaveChanges();

            foreach (var privilege in GetPrivilege(item.Privileges.Privilege))
            {
                var privilegeItem = new Privileges
                {
                    Privilege = privilege.Trim(),
                    HotelRoomId = item.Id
                };
               _previlegesRepository.Add(privilegeItem);
            }
            _photoRoomsRepository.PhotoUpload(item);


			return item;

        }

        List<string> GetPrivilege(string privileges)
        {
            var list = privileges.Split(';').ToList();

            for(int i = 0;i <  list.Count(); i++)
            {
                if (string.IsNullOrWhiteSpace(list.ElementAt(i)))
                {
                    list.Remove(list.ElementAt(i));
                }
            }

            return list;
        }

        public HotelRoom Delete(int id)
        {
            var item = _context.HotelRooms.FirstOrDefault(x => x.Id == id);
            _context.HotelRooms.Remove(item);
            _context.SaveChanges();
            return item;
        }
        
        public HotelRoom Get(int id)
        {
			HotelRoom room = _context.HotelRooms
						.Include(r => r.Images) 
						.Include(r => r.Hotel)
                        .Include(r => r.PrivilegesList)
						.FirstOrDefault(r => r.Id == id);
            
			var privileges = _context.Privileges
	            .Where(x => x.HotelRoomId == id)
	            .Select(item => item.Privilege);

			var privilegesString = string.Join(";", privileges);

            room.Privileges = new Privileges
            {
                HotelRoomId = id,
                Privilege = privilegesString,        
            };

            room.Image = room.Images.FirstOrDefault();

            return room;

        }

        public IEnumerable<HotelRoom> GetAll(int hotelId, int NumberOfPeople = 0)
        {
			IQueryable<HotelRoom> query = _context.HotelRooms.Include(r => r.Images);

			if (NumberOfPeople != 0)
			{
				query = query.Where(r => r.NumberOfPeople >= NumberOfPeople);
			}
			else
			{
				query = query.Where(r => r.HotelId == hotelId);
			}

			List<HotelRoom> all = query.ToList();

			foreach (var item in all)
			{
				item.Image = item.Images.FirstOrDefault();
			}
			return all;

        }

        public HotelRoom Update(HotelRoom item)
        {
            _context.Privileges.RemoveRange(_context.Privileges.Where(x => x.HotelRoomId == item.Id));

			foreach (var privilege in GetPrivilege(item.Privileges.Privilege))
			{
				var privilegeItem = new Privileges
				{
					Privilege = privilege.Trim(),
					HotelRoomId = item.Id
				};
				_previlegesRepository.Add(privilegeItem);
			}
			_context.Update(item);
            
            _context.SaveChanges();
            return item;
        }




    }
}

