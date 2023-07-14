using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces.IUsersRepo
{
	public interface IUsersRepository
	{
		public IdentityUser Get(string id);
	}
}
