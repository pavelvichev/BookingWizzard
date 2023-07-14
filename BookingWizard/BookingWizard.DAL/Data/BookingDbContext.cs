using BookingWizard.DAL.Entities;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Entities.Hotels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BookingWizard.DAL.Data
{
    public class BookingDbContext : DbContext
	{

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
		{
            
        }



		public DbSet<Hotel> hotels { get; set; }
		public DbSet<HotelRoom> hotelRooms { get; set; }
		public DbSet<Address> Address { get; set; }
		public DbSet<Booking> Booking { get; set; }
		public DbSet<HotelImages> HotelImages { get; set; }
		public DbSet<RoomImages> RoomImages { get; set; }
		public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}


	}

}
