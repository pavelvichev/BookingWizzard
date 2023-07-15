using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities.Hotels
{
	public class Review
	{
		public int Id { get; set; }

		[Required]

		public string Text { get; set; }

		[DataType(DataType.Date)]

		public DateTime ReviewDate { get; set; }

		[Range(1, 5)]

		public int Rating { get; set; }

		public string IdentityUserId { get; set; }

		[NotMapped]
		public IdentityUser User { get; set; }

		public int HotelId { get; set; }

		public Hotel Hotel { get; set; }
	}
}
