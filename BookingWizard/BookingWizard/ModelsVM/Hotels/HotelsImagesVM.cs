using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.ModelsVM.Hotels
{
    public class HotelImagesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }
    }
}
