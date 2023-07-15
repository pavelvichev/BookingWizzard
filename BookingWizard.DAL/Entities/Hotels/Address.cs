using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities.Hotels
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressName { get; set; }
        public int HotelId { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }

    }
}
