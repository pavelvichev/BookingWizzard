using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces.IHotelRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Repositories.HotelRepo
{
    public class PhotoHotelRepository : IPhotoHotelsRepository
    {

        readonly BookingDbContext _context;
        readonly IStringLocalizer<PhotoHotelRepository> _localizer;

        public PhotoHotelRepository(BookingDbContext context, IStringLocalizer<PhotoHotelRepository> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public void PhotoUpload(Hotel hotel)
        {
            foreach (var file in hotel.ImageModelList)
            {
                if (file != null && file.Length > 0)
                {
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + hotel.HotelName + "-" + file.FileName;
                    var photo = new HotelImages
                    {
                        Name = uniqueFileName,
                        ImageData = imageData,
                        HotelId = hotel.Id
                    };

                    _context.HotelImages.Add(photo);
                    _context.SaveChanges();

                }
            }
        }
        public void DeletePhoto(int id, int HotelId)
        {

            var item = _context.HotelImages.Select(u => u).Where(x => x.Id == id).FirstOrDefault();

            if (_context.HotelImages.Select(u => u).Where(x => x.HotelId == HotelId).Count() != 1)
            {
                _context.HotelImages.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception(_localizer["OnePhotoLeft"]);
            }
        }

        public HotelImages GetPhoto(int id)
        {
            var photo = _context.HotelImages.FirstOrDefault(p => p.Id == id);

            return photo;
        }
    }
}
