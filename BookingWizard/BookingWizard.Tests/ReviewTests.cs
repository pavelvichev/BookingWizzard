using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces.IUsersRepo;
using BookingWizard.DAL.Repositories.HotelRepo;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Tests
{
	public class ReviewTests
	{
		Review CreateReview(int Id = 0, string textReview = "", string UserId = "", int ratingRev = 0, int HotelId = 0)
		{
			// Arrange
			var id = Id == 0 ? 1 : Id;
			var text = textReview == "" ? "This is a review" : textReview;
			var reviewDate = DateTime.Now;
			var rating = ratingRev == 0 ? 4 : ratingRev ;
			var identityUserId = UserId == "" ?  "user123" : UserId;
			var hotelId = HotelId == 0 ? 1 : HotelId;

			// Act
			var review = new Review
			{
				Id = id,
				Text = text,
				ReviewDate = reviewDate,
				Rating = rating,
				IdentityUserId = identityUserId,
				HotelId = hotelId,
			};

			return review;
		}

		[Fact]
		public void Add_ValidReview_AddsReviewToContext()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "ReviewDatabase")
				.Options;

			var review = CreateReview();

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				var mockUsersRepository = new Mock<IUsersRepository>();
				var repository = new ReviewRepository(context, mockUsersRepository.Object);

				// Act
				var result = repository.Add(review);

				// Assert
				Assert.Equal(review, result);

				// Verify that the review is added to the context
				Assert.Contains(review, context.Reviews);
			}
		}

		[Fact]
		public void Update_ExistingReview_UpdatesReviewInContext()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "ReviewDatabase1")
				.Options;

			var existingReview = CreateReview();

			using (var context = new BookingDbContext(options))
			{
				context.Reviews.Add(existingReview);
				context.SaveChanges();

				var mockUsersRepository = new Mock<IUsersRepository>();
				var repository = new ReviewRepository(context, mockUsersRepository.Object);

				// Act
				existingReview.Text = "uaggagagaa";
				repository.Update(existingReview);

				// Assert
				// Verify that the review is updated in the context
				var updatedReview = context.Reviews.FirstOrDefault(r => r.Id == existingReview.Id);

				Assert.NotNull(updatedReview);
				Assert.Equal("uaggagagaa", updatedReview.Text);
			}
		}

		[Fact]
		public void GetAll_ValidHotelId_ReturnsReviewsForHotel()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "ReviewDatabase2")
				.Options;

			var hotelId = 1;
			var reviews = new List<Review>
			{
				CreateReview(Id: 1, HotelId: 1),
				CreateReview(Id: 2, HotelId: 2 ),
				CreateReview(Id : 3, HotelId : 1),
            };

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.Reviews.AddRange(reviews);
				context.SaveChanges();

				var mockUsersRepository = new Mock<IUsersRepository>();
				var repository = new ReviewRepository(context, mockUsersRepository.Object);

				// Act
				var result = repository.GetAll(hotelId);

				// Assert
				Assert.Equal(2, result.Count());
				Assert.NotEqual(reviews.Count, result.Count());
				Assert.All(result, r => Assert.Equal(hotelId, r.HotelId));
			}
		}

		[Fact]
		public void Delete_ExistingReview_RemovesReviewFromContext()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "ReviewDatabase3")
				.Options;

			var existingReview = CreateReview();

			using (var context = new BookingDbContext(options))
			{
				context.Reviews.Add(existingReview);
				context.SaveChanges();

				var mockUsersRepository = new Mock<IUsersRepository>();
				var repository = new ReviewRepository(context, mockUsersRepository.Object);

				// Act
				repository.Delete(existingReview.Id);

				// Assert
				// Verify that the review is removed from the context
				var deletedReview = context.Reviews.FirstOrDefault(r => r.Id == existingReview.Id);
				Assert.DoesNotContain(existingReview, context.Reviews);
				Assert.Null(deletedReview);
			}
		}

	}
}
