using BookingWizard.DAL.Entities.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces.IHotels
{
    public interface IPhotoHotelsService
    {
        public void PhotoUpload(Hotel hotel);
        public void DeletePhoto(int id, int HotelId);
        public HotelImages GetPhoto(int id);
    }
}
