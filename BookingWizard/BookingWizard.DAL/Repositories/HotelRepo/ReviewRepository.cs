using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces.IHotelRepo;
using BookingWizard.DAL.Interfaces.IUsersRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Repositories.HotelRepo
{
	public class ReviewRepository : IReviewsRepository
	{
		BookingDbContext _context;
		IUsersRepository _usersRepository;
		public ReviewRepository(BookingDbContext context, IUsersRepository usersRepository)
		{
			_context = context;
			_usersRepository = usersRepository;
		}

		public Review Add(Review review)
		{
			 _context.Reviews.Add(review);
			_context.SaveChanges();
			return review;
		}

		public void Update(Review review)
		{
			 _context.Reviews.Update(review);
			_context.SaveChanges();
		}
		
		public IEnumerable<Review> GetAll(int hotelId)
		{
			var all = _context.Reviews
				.Where(x => x.HotelId == hotelId)
				.ToList();


			foreach (var item in all)
			{
				item.User = new IdentityUser();
				item.User = _usersRepository.Get(item.IdentityUserId);
			}
			return all;
		}

		public void Delete(int reviewId)
		{
			var review = _context.Reviews.FirstOrDefault(x => x.Id == reviewId);

			_context.Reviews.Remove(review);
			_context.SaveChanges();
		}
	}
}
