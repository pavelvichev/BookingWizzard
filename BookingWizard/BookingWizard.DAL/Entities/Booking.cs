using BookingWizard.DAL.Entities;
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

			public DateTime ArrivalDate { get; set; }
			public DateTime DateOfDeparture { get; set; }
		}
	
}
