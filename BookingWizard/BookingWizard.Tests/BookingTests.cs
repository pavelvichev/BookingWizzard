using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Repositories.BookingRepo;
using BookingWizard.DAL.Repositories.HotelRoomsRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Tests
{
	public class BookingTests
	{
		public Booking CreateBooking(DateTime arrival, DateTime departure, string userId = "", int id = 0)
		{
			// Arrange
			var bookId = id == 0 ? 0 : id;
			var roomId = 1;
			var roomName = "Test Room";
			var hotelName = "Test Hotel";
			var arrivalDate = arrival;
			var departureDate = departure;
			uint allPrice = 500;
			var firstName = "John";
			var lastName = "Doe";
			var email = "john.doe@example.com";
			var IdentityUserId = (userId == "" ? "testuser123" : userId);

			// Act
			var booking = new Booking
			{
				RoomId = roomId,
				RoomName = roomName,
				HotelName = hotelName,
				ArrivalDate = arrivalDate,
				DateOfDeparture = departureDate,
				allPrice = allPrice,
				FirstNameBuyer = firstName,
				LastNameBuyer = lastName,
				Email = email,
				IdentityUserId = IdentityUserId
			};

			return booking;
		}

			[Fact]
			public void Add_ValidBooking_AddsBookingToContext()
			{
				// Arrange
				var options = new DbContextOptionsBuilder<BookingDbContext>()
					.UseInMemoryDatabase(databaseName: "BookingDatabase1")
					.Options;
				
				var booking = CreateBooking(new DateTime(2023, 7, 15), new DateTime(2023, 7, 20));
			
				
				using (var context = new BookingDbContext(options))
				{
					
					context.Database.EnsureDeleted();

					var mockLocalizer = new Mock<IStringLocalizer<BookingRepository>>();
					var bookingRepository = new BookingRepository(context, mockLocalizer.Object);
					var mockStringLocalizer = new Mock<IStringLocalizer<HotelRoomRepository>>();
					var roomRepository = new HotelRoomRepository(context, null);

					HotelRoomsTests tests = new HotelRoomsTests();
					tests.Add_ReturnsAddedHotelRoom();


					// Act
					var result = bookingRepository.Add(booking);

					// Assert
					Assert.Equal(booking, result);
					Assert.Contains(booking, context.Booking);
				}
			}

			[Fact]
			public void Add_OverlappingBooking_ThrowsException()
			{
				// Arrange
				var options = new DbContextOptionsBuilder<BookingDbContext>()
					.UseInMemoryDatabase(databaseName: "BookingDatabase2")
					.Options;

				var existingBooking = CreateBooking(new DateTime(2023, 7, 15), new DateTime(2023, 7, 20));

				var newBooking = CreateBooking(new DateTime(2023, 7, 16), new DateTime(2023, 7, 18));

				using (var context = new BookingDbContext(options))
				{
					context.Database.EnsureDeleted();

					context.Booking.Add(existingBooking);
					context.SaveChanges();

					var mockLocalizer = new Mock<IStringLocalizer<BookingRepository>>();
					var repository = new BookingRepository(context, mockLocalizer.Object);

					// Act and Assert
					Assert.Throws<Exception>(() => repository.Add(newBooking));
				}
			}

			[Fact]
			public void Delete_RemovesBookingFromContext()
			{
				// Arrange
				var options = new DbContextOptionsBuilder<BookingDbContext>()
					.UseInMemoryDatabase(databaseName: "BookingDatabase3")
					.Options;

				var booking = CreateBooking(new DateTime(2023, 7, 15), new DateTime(2023, 7, 20));

				using (var context = new BookingDbContext(options))
				{
					context.Database.EnsureDeleted();

					context.Booking.Add(booking);
					context.SaveChanges();

					var mockLocalizer = new Mock<IStringLocalizer<BookingRepository>>();
					var repository = new BookingRepository(context, mockLocalizer.Object);

					// Act
					repository.Delete(booking.Id);

					// Assert
					Assert.DoesNotContain(booking, context.Booking);
				}
			}

			[Fact]
			public void GetAll_WithUserId()
			{
			//Arrange

			var bookings = new List<Booking>
			{
				CreateBooking(new DateTime(2023, 7, 15), new DateTime(2023, 7, 20), "hello", 1),
				CreateBooking(new DateTime(2023, 7, 7), new DateTime(2023, 7, 26),"first", 2),
				CreateBooking(new DateTime(2023, 7, 17), new DateTime(2023, 7, 9), "hello", 3),
			};


			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "BookingDatabase2")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.Booking.AddRange(bookings);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<BookingRepository>>();
				var repository = new BookingRepository(context, null);

				//Act
				var result = repository.GetAll("hello");


				//Assert

				Assert.NotNull(result);
				Assert.Equal(2, result.Count());
				Assert.Equal(1, result.First().Id);
				Assert.Equal(3, result.Last().Id);

			}
		}

			[Fact]
			public void GetBooking_WithId()
			{
			//Arrange
			

			var bookings = new List<Booking>
			{
				CreateBooking(new DateTime(2023, 7, 15), new DateTime(2023, 7, 20), "hello", 1),
				CreateBooking(new DateTime(2023, 7, 7), new DateTime(2023, 7, 26),"first", 2),
				CreateBooking(new DateTime(2023, 7, 17), new DateTime(2023, 7, 9), "hello", 3),
			};


			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "BookingDatabase2")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.Booking.AddRange(bookings);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<BookingRepository>>();
				var repository = new BookingRepository(context, null);

				//Act
				var result = repository.Get(1);


				//Assert

				Assert.NotNull(result);
				Assert.Equal("hello", result.IdentityUserId);


			}
		}

			[Fact]
			public void Update_UpdateingBooking()
			{
				//Arrange

				var booking = CreateBooking(new DateTime(2023, 7, 15), new DateTime(2023, 7, 20), "hello", 1);
					
				var options = new DbContextOptionsBuilder<BookingDbContext>()
					.UseInMemoryDatabase(databaseName: "BookingDatabase2")
					.Options;

				using (var context = new BookingDbContext(options))
				{
					context.Database.EnsureDeleted();

					context.Booking.AddRange(booking);
					context.SaveChanges();

					var mockStringLocalizer = new Mock<IStringLocalizer<BookingRepository>>();
					var repository = new BookingRepository(context, null);

					booking.IdentityUserId = "Update hello";

					//Act

					var result = repository.Update(booking);


					//Assert

					Assert.Null(result);
					Assert.Equal(booking.Id, result.Id);
					Assert.Equal("Update hello", result.IdentityUserId);


			}
		}

		}
}
