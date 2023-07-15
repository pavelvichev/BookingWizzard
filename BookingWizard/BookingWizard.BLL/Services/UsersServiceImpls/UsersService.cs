using BookingWizard.BLL.Interfaces.IUsersServices;
using BookingWizard.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services.UsersServiceImpls
{

	public class UsersService : IUsersService
	{
		IUnitOfWork _unitOfWork;
		public UsersService(IUnitOfWork unitOfWork)
		{
			_unitOfWork= unitOfWork;
		}

		public IdentityUser Get(string id)
		{
			return _unitOfWork.Users.Get(id);
		}
	}
}
