using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities.HotelRooms
{
    public class Privileges
    {
        public int Id { get; set; }
        public string Privilege { get; set; }
        public int HotelRoomId { get; set; }
    }
}
