using BookingWizard.DAL.Data;
using BookingWizard.DAL.Interfaces.IUsersRepo;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Repositories.UsersRepo
{
	public class UsersRepository : IUsersRepository
	{
		BookingDbContext _context;
		IdentityServerContext _identityContext;
	
		public UsersRepository(BookingDbContext context, IdentityServerContext identityContext)
		{
			_context = context;
			_identityContext = identityContext;
		}

		public IdentityUser Get(string id)
		{
			IdentityUser user = _identityContext.Users.Where(x => x.Id == id).FirstOrDefault();

			return user;
		}
	}
}
