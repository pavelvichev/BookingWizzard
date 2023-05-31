using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingWizard.DAL.Data.EntityTypeConfiguration
{
	internal class BookingConfigure : IEntityTypeConfiguration<Booking>
	{
		public void Configure(EntityTypeBuilder<Booking> builder)
		{
			builder
			 .HasOne(h => h.Room)
			 .WithMany(b => b.Bookings)
			 .HasForeignKey(h => h.RoomId)
			 .IsRequired()
			 .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
