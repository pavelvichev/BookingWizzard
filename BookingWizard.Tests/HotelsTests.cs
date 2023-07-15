using AutoMapper;
using BookingWizard.BLL.Services.HotelsServiceImpls;
using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Repositories.HotelRepo;
using BookingWizard.Infrastructure;
using BookingWizard.ModelsVM.Hotels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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

	public class HotelsTests
	{
		[Fact]
		public void GetAll_WithUserId_ReturnsFilteredHotels()
		{
			// Arrange
			var userId = "user123";
			var hotels = new List<Hotel>
				{
					new Hotel {
						Id = 1,
						IdentityUserId = "user123" ,
						HotelLongDescription = "Some long description 1 ",
						HotelName = "Hotel Name 1",
						HotelShortDescription = "Short description 1"
					},
					new Hotel {
						Id = 2,
						IdentityUserId = "user456",
						HotelLongDescription = "Some long description 2 ",
						HotelName = "Hotel Name 2",
						HotelShortDescription = "Short description 2"},
					new Hotel {
						Id = 3,
						IdentityUserId = "user123",
						HotelLongDescription = "Some long description 3 ",
						HotelName = "Hotel Name 3",
						HotelShortDescription = "Short description 3"
					}
				};

			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "HotelsDatabase1")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.Hotels.AddRange(hotels);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRepository>>();
				var repository = new HotelRepository(context, mockStringLocalizer.Object, null);

				// Act
				var result = repository.GetAll(userId);

				// Assert
				Assert.Equal(2, result.Count());
				Assert.Equal(1, result.First().Id);
				Assert.Equal(3, result.Last().Id);

			}
		}

		[Fact]
		public void GetAll_WithoutUserId_ReturnsAllHotels()
		{
			// Arrange
			var hotels = new List<Hotel>
				{
					new Hotel {
						Id = 1,
						IdentityUserId = "user123" ,
						HotelLongDescription = "Some long description 1 ",
						HotelName = "Hotel Name 1",
						HotelShortDescription = "Short description 1"
					},
					new Hotel {
						Id = 2,
						IdentityUserId = "user456",
						HotelLongDescription = "Some long description 2 ",
						HotelName = "Hotel Name 2",
						HotelShortDescription = "Short description 2"},
					new Hotel {
						Id = 3,
						IdentityUserId = "user123",
						HotelLongDescription = "Some long description 3 ",
						HotelName = "Hotel Name 3",
						HotelShortDescription = "Short description 3"
					}
				};

			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "HotelDatabase")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();
				context.Hotels.AddRange(hotels);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRepository>>();
				var repository = new HotelRepository(context, mockStringLocalizer.Object, null);

				// Act
				var result = repository.GetAll();

				// Assert
				Assert.Equal(3, result.Count());
				Assert.Equal(1, result.First().Id);
				Assert.Equal(3, result.Last().Id);


			}

		}


		[Fact]
		public void Add_ReturnsAddedRoom()
		{
			// Arrange

			var formFileMock = new Mock<IFormFile>();
			var formFileContent = Encoding.UTF8.GetBytes("Test image data");
			var stream = new MemoryStream(formFileContent);
			formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);
			formFileMock.Setup(f => f.FileName).Returns("test.jpg");
			formFileMock.Setup(f => f.Length).Returns(formFileContent.Length);

			var hotel = new Hotel
			{
				IdentityUserId = "user123",
				HotelLongDescription = "Some long description",
				HotelName = "Hotel Name",
				HotelShortDescription = "Short description",
				ImageModelList = new List<IFormFile>
				{
					formFileMock.Object,
				}
				
			};

			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "HotelsDatabase1")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();
			
				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRepository>>();
				var mockPhotoStringLocalizer = new Mock<IStringLocalizer<PhotoHotelRepository>>();

				var photoHotelRepository = new PhotoHotelRepository(context, mockPhotoStringLocalizer.Object);

				var repository = new HotelRepository(context, mockStringLocalizer.Object, photoHotelRepository);
				// Act

				var result = repository.Add(hotel);

				// Assert
				Assert.NotNull(result);
				Assert.Equal(hotel.Id, result.Id);
				Assert.Equal(hotel.HotelName, result.HotelName);

			}
		}

		[Fact]
		public void Delete_RemovesHotel()
		{
			// Arrange
			var hotelId = 1;
			var hotels = new List<Hotel>
				{
					new Hotel {
						Id = 1,
						IdentityUserId = "user123" ,
						HotelLongDescription = "Some long description 1 ",
						HotelName = "Hotel Name 1",
						HotelShortDescription = "Short description 1",
						Address = new Address
						{
							AddressName = "address",
							Lat = 48.7787f,
							Lng = 50.7787f,
						},
						Images = new List<HotelImages>
						{
						new HotelImages
						{
							Name = "Hello",
							ImageData = new byte[] { 0x12, 0x34, 0x56, 0x78 }
						},
						new HotelImages
						{
							Name = "Hi",
							ImageData = new byte[] { 0x22, 0x34, 0x76, 0x78 }
						}
				}
					},
					new Hotel {
						Id = 2,
						IdentityUserId = "user456",
						HotelLongDescription = "Some long description 2 ",
						HotelName = "Hotel Name 2",
						HotelShortDescription = "Short description 2"},
					new Hotel {
						Id = 3,
						IdentityUserId = "user123",
						HotelLongDescription = "Some long description 3 ",
						HotelName = "Hotel Name 3",
						HotelShortDescription = "Short description 3"
					}
				};

			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "HotelsDatabase2")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.Hotels.AddRange(hotels);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRepository>>();
				var repository = new HotelRepository(context, mockStringLocalizer.Object, null);

				// Act
				 repository.Delete(hotelId);

				// Assert
				var remainingHotels = context.Hotels.ToList();
				var remainingImages = context.HotelImages.Where(x => x.HotelId == hotelId);
				var remainingAddresses = context.Address.Where(x => x.HotelId == hotelId);
				Assert.DoesNotContain(remainingHotels, h => h.Id == hotelId);
				Assert.Empty(remainingImages);
				Assert.Empty(remainingAddresses);
				
			}
		}

		[Fact]
		public void Get_ReturnsHotelWithRelatedData()
		{
			// Arrange
			var hotelId = 1;
			var hotel = new Hotel
			{
				Id = hotelId,
				IdentityUserId = "user123",
				HotelLongDescription = "Some long description",
				HotelName = "Hotel Name",
				HotelShortDescription = "Short description",
				Address = new Address
				{
					AddressName = "address",
					Lat = 48.7787f,
					Lng = 50.7787f,
				},
				Images = new List<HotelImages>
				{
					new HotelImages 
					{ 
						Name = "Hello", 
						ImageData = new byte[] { 0x12, 0x34, 0x56, 0x78 }  
					},
					new HotelImages 
					{
						Name = "Hi",
						ImageData = new byte[] { 0x22, 0x34, 0x76, 0x78 }
					}
				}
			};

			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "HotelsDatabase2")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Hotels.Add(hotel);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRepository>>();
				var repository = new HotelRepository(context, mockStringLocalizer.Object, null);

				// Act
				var result = repository.Get(hotelId);

				// Assert
				Assert.NotNull(result);
				Assert.Equal(hotelId, result.Id);
				Assert.Equal(hotel.IdentityUserId, result.IdentityUserId);
				Assert.Equal(hotel.HotelLongDescription, result.HotelLongDescription);
				Assert.Equal(hotel.HotelName, result.HotelName);
				Assert.Equal(hotel.HotelShortDescription, result.HotelShortDescription);
				Assert.NotNull(result.Address);
				Assert.Equal(hotel.Address.Lat, result.Address.Lat); 
				Assert.Equal(hotel.Address.Lng, result.Address.Lng); 					
				Assert.Equal(hotel.Address.AddressName, result.Address.AddressName); 												
				Assert.NotNull(result.Images);
				Assert.Equal(hotel.Images.Count, result.Images.Count);
				for (int i = 0; i < hotel.Images.Count; i++)
				{
					Assert.Equal(hotel.Images.ElementAt(i), result.Images.ElementAt(i)); 

				}
			}
		}

		[Fact]
		public void Update_ReturnsUpdatedHotel()
		{
			// Arrange
			var hotel = new Hotel
			{
				Id = 1,
				IdentityUserId = "user123",
				HotelLongDescription = "Some long description",
				HotelName = "Hotel Name",
				HotelShortDescription = "Short description"
			};

			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "HotelsDatabase")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();
				context.Hotels.Add(hotel);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRepository>>();
				var repository = new HotelRepository(context, mockStringLocalizer.Object, null);

				// Act
				hotel.HotelName = "Updated Hotel Name";
				var result = repository.Update(hotel);

				// Assert
				Assert.NotNull(result);
				Assert.Equal(hotel.Id, result.Id);
				Assert.Equal("Updated Hotel Name", result.HotelName);
			}
		}

	}
}

