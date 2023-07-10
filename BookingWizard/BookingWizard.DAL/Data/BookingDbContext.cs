using BookingWizard.DAL.Entities;
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
		public DbSet<hotelRoom> hotelRooms { get; set; }
		public DbSet<Address> Address { get; set; }
		public DbSet<Booking> Booking { get; set; }
		public DbSet<HotelImages> HotelImages { get; set; }
		public DbSet<RoomImages> RoomImages { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> UkResources { get; set; }
        public DbSet<Resource> EnResources { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}


	}

}
