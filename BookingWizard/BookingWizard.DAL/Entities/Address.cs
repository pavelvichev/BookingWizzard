using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities
{
	public class Address
	{
		public int Id { get; set; }
		public string Addres { get; set; }
		public Hotel Hotel { get; set; }
		public int HotelId { get; set; }




	}
}
