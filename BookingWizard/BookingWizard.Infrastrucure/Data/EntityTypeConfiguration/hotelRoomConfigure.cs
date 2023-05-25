using BookingWizard.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Infrastrucure.Data.EntityTypeConfiguration
{
	internal class hotelRoomConfigure : IEntityTypeConfiguration<hotelRoom>
	{
		public void Configure(EntityTypeBuilder<hotelRoom> builder)
		{

			builder
				 .HasOne(h => h.Booking)
				 .WithOne(b => b.Room)
				 .HasForeignKey<Booking>(h => h.RoomId)
				 .IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
