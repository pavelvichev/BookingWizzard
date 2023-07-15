using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Repositories.HotelRoomsRepo
{
    public class PhotoRoomRepository : IPhotoRoomsRepository
    {
        readonly BookingDbContext _context;
        readonly IStringLocalizer<PhotoRoomRepository> _localizer;

        public PhotoRoomRepository(BookingDbContext context, IStringLocalizer<PhotoRoomRepository> localizer)
        {
            _context = context;
            _localizer = localizer;
        }


        public void PhotoUpload(HotelRoom room)
        {
            foreach (var file in room.ImageModelList)
            {
                if (file != null && file.Length > 0)
                {
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + room.Name + "-" + file.FileName;
                    var photo = new RoomImages
                    {
                        Name = uniqueFileName,
                        ImageData = imageData,
                        RoomId = room.Id
                    };

                    _context.RoomImages.Add(photo);
                    _context.SaveChanges();
                }
            }
        }

        public void DeletePhoto(int id, int roomId)
        {
            var item = _context.RoomImages.Select(u => u).Where(x => x.Id == id).FirstOrDefault();
            if (!(_context.RoomImages.Select(u => u).Where(x => x.RoomId == roomId).Count() == 1))
            {
                _context.RoomImages.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception(_localizer["OnePhotoLeft"]);
            }
        }

        public RoomImages GetPhoto(int id)
        {
            var photo = _context.RoomImages.FirstOrDefault(p => p.Id == id);


            return photo; // ил			
        }
    }
}
