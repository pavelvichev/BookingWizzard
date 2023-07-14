using AutoMapper;
using BookingWizard.BLL.Interfaces.IHotelsServices;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.ModelsVM.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingWizard.Controllers.Hotels
{
	public class ReviewsController : Controller
	{
		IReviewsService _reviewsService;
		IMapper _map;
		public ReviewsController(IReviewsService reviewsService, IMapper mapper)
		{
			_reviewsService = reviewsService;
			_map = mapper;
		}
		[Authorize]
		public IActionResult AddReview(ReviewVM reviewVM)
		{

			reviewVM.ReviewDate = DateTime.Now;
			if (ModelState.IsValid)
			{
				_reviewsService.Add(_map.Map<Review>(reviewVM));

			}

			return RedirectToAction("Hotel", "Hotels", new { id = reviewVM.HotelId });

		}

		public IActionResult EditReview(ReviewVM review)
		{
			if (ModelState.IsValid)
			{
				_reviewsService.Update(_map.Map<Review>(review));
                return Ok();
            }

			return BadRequest();
			
		}

		public IActionResult DeleteReview(int reviewId, int hotelId)
		{
				_reviewsService.Delete(reviewId);

			return RedirectToAction("Hotel", "Hotels", new { id = hotelId });
			
		}


	}
}
