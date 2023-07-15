using BookingWizard.DAL.Entities.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Interfaces.IHotelRepo
{
	public interface IReviewsRepository
	{
		public Review Add(Review review); 
		public IEnumerable<Review> GetAll(int hotelId); 
		public void Update(Review review); 
		public void Delete(int reviewId); 
	}
}
