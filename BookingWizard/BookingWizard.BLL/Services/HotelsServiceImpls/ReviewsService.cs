using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Services.HotelsServiceImpls
{
	public class ReviewsService : IReviewsService
	{
		IUnitOfWork _unitOfWork;

		public ReviewsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public Review Add(Review review)
		{
			return _unitOfWork.Reviews.Add(review);
		}
		public IEnumerable<Review> GetAll(int hotelId)
		{
			return _unitOfWork.Reviews.GetAll(hotelId);
		}

        public void Update(Review review)
        {
             _unitOfWork.Reviews.Update(review);
        }
		public void Delete(int reviewId)
        {
             _unitOfWork.Reviews.Delete(reviewId);
        }
    }
}
