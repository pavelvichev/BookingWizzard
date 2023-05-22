using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Core.Entities
{
		public class Booking
		{
			public int Id { get; set; }

			public hotelRoom room { get; set; }

			public DateTime arrival_date { get; set; }
			public DateTime date_of_departure { get; set; }
		}
	
}
