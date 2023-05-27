using BookingWizard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Infrastrucure.Data.EntityTypeConfiguration
{
	internal class HotelConfigure : IEntityTypeConfiguration<Hotel>
	{
		public void Configure(EntityTypeBuilder<Hotel> builder)
		{
			builder
				.HasMany(h => h.roomList)
				.WithOne(r => r.Hotel)
				.HasForeignKey(r => r.HotelId);

			builder
				.HasOne(h => h.address)
				.WithMany(a => a.HotelList)
				.HasForeignKey(k => k.addressId);
		}

	}
}
