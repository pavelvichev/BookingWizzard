using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Data
{
	public class IdentityServerContext : IdentityDbContext
	{
		public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options) { }
	}
}
