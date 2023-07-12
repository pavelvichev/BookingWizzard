using BookingWizard.DAL.Entities.Hotels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Data.EntityTypeConfiguration
{
    internal class AddressConfigure : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasOne(x => x.Hotel)
                .WithOne(x => x.Address)
                .HasForeignKey<Address>(x => x.HotelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
                
                }
    }
}
