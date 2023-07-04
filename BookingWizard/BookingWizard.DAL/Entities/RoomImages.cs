using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities
{
    public class RoomImages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public hotelRoom Room { get; set; }
        public int RoomId { get; set; }
    }
}
