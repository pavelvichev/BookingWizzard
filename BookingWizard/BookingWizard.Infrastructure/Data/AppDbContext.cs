using Microsoft.EntityFrameworkCore;

namespace BookingWizard.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Hotel> hotels { get; set; }
        public DbSet<hotelRoom> hotelRooms { get; set; }
        public DbSet<Address> Address { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Hotel>()
				.HasMany(h => h.roomList)
				.WithOne(r => r.Hotel)
				.HasForeignKey(r => r.HotelId);

			// Other configurations

			base.OnModelCreating(modelBuilder);
		}
	}

}
