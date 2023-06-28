using BookingWizard.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Data.EntityTypeConfiguration
{

    internal class hotelRoomConfigure : IEntityTypeConfiguration<hotelRoom>
    {
        public void Configure(EntityTypeBuilder<hotelRoom> builder)
        {
            builder
             .HasOne(h => h.Hotel)
             .WithMany(b => b.roomList)
             .HasForeignKey(h => h.HotelId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

