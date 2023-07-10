using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public hotelRoom Room { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string HotelName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public uint allPrice { get; set; }
        public string FirstNameBuyer { get; set; }
        public string LastNameBuyer { get; set; }
        public string Email { get; set; }
        public string IdentityUserId { get; set; }
    }

}
