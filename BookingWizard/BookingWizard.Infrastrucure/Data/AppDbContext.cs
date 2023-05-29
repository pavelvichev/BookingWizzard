using BookingWizard.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingWizard.Infrastrucure.Data
{
    public class AppDbContext : DbContext
    {
		
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		


		public DbSet<Hotel> hotels { get; set; }
        public DbSet<hotelRoom> hotelRooms { get; set; }
        public DbSet<Address> Address { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}


	}

}
