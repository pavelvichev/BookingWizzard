using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingWizard.DAL.Entities.HotelRooms;

namespace BookingWizard.DAL.Data.EntityTypeConfiguration
{

    internal class hotelRoomConfigure : IEntityTypeConfiguration<HotelRoom>
    {
        public void Configure(EntityTypeBuilder<HotelRoom> builder)
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

