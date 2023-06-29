using BookingWizard.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingWizard.DAL.Data
{
	public class BookingDbContext : DbContext
	{

		public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }



		public DbSet<Hotel> hotels { get; set; }
		public DbSet<hotelRoom> hotelRooms { get; set; }
		public DbSet<Address> Address { get; set; }
		public DbSet<Booking> Booking { get; set; }
		public DbSet<HotelImages> HotelImages { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}


	}

}
