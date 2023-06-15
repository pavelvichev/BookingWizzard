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

			public DateTime arrival_date { get; set; }
			public DateTime date_of_departure { get; set; }
		}
	
}
