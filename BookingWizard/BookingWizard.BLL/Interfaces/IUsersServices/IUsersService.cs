using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces.IUsersServices
{
	public interface IUsersService
	{
		public IdentityUser Get(string id);
	}
}
