using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities.Hotels
{
    public class HotelImages
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public byte[] ImageData { get; set; }

        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }

    }
}
