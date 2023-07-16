using BookingWizard.DAL.Data;
using BookingWizard.DAL.Entities.HotelRooms;
using BookingWizard.DAL.Entities.Hotels;
using BookingWizard.DAL.Interfaces.IHotelRoomsRepo;
using BookingWizard.DAL.Repositories.HotelRepo;
using BookingWizard.DAL.Repositories.HotelRoomsRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingWizard.Tests
{
	public class HotelRoomsTests
	{
		[Fact]
		public void GetAll_WithHotelIdWithoutNumberOfPeople()
		{
			//Arrange
			int hotelId = 1;

			var rooms = new List<HotelRoom>
			{
				new HotelRoom
				{
					HotelId = 1,
					Id = 1,
					Name= "Test 1",
					Description = "Desc 1",
					RoomPricePerNight = 123,
					Images = new List<RoomImages>
					{
						new RoomImages
						{
							Name = "Hello1",
							ImageData = new byte[] { 0x12, 0x34, 0x56, 0x78 }
						},
						new RoomImages
						{
							Name = "Hi1",
							ImageData = new byte[] { 0x22, 0x34, 0x76, 0x78 }
						},
					},
					NumberOfPeople = 3,
					PrivilegesList = new List<Privileges>
					{
						new Privileges
						{
							HotelRoomId = 1,
							Id = 1,
							Privilege = "Вид на море",
						}
					}
						
                }
				,new HotelRoom
				{
					Id = 2,
					HotelId = 2,
					Name= "Test 2",
					Description = "Desc 2",
					RoomPricePerNight = 78,
				}
				,new HotelRoom
				{
					HotelId = 1,
					Id = 3,
					Name= "Test 3",
					Description = "Desc 3",
					RoomPricePerNight = 98,
				},
			};


			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.HotelRooms.AddRange(rooms);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRoomRepository>>();
				var mockPrivilegesRepository = new Mock<PrivilegesRepository>(context);
				var mockPhotoRoomsRepository = new Mock<IPhotoRoomsRepository>();
				var repository = new HotelRoomRepository(context, mockPhotoRoomsRepository.Object, mockPrivilegesRepository.Object);

				//Act
				var result = repository.GetAll(hotelId);


				//Assert

				Assert.NotNull(result);
				Assert.NotNull(result.FirstOrDefault().Images);
				Assert.Equal(2, result.FirstOrDefault().Images.Count());
				Assert.NotNull(result.FirstOrDefault().PrivilegesList);
                Assert.Equal(1, result.FirstOrDefault().PrivilegesList.Count());
                Assert.Equal(2, result.Count());
				Assert.Equal(1, result.First().Id);
				Assert.Equal(3, result.Last().Id);

			}
		}
		[Fact]
		public void GetAll_WithHotelIdAndNumberOfPeople()
		{
			//Arrange
			int hotelId = 1;
			int NumberOfPeople = 2;

            var rooms = new List<HotelRoom>
            {
                new HotelRoom
                {
                    HotelId = 1,
                    Id = 1,
                    Name= "Test 1",
                    Description = "Desc 1",
                    RoomPricePerNight = 123,
                    Images = new List<RoomImages>
                    {
                        new RoomImages
                        {
                            Name = "Hello1",
                            ImageData = new byte[] { 0x12, 0x34, 0x56, 0x78 }
                        },
                        new RoomImages
                        {
                            Name = "Hi1",
                            ImageData = new byte[] { 0x22, 0x34, 0x76, 0x78 }
                        },
                    },
                    NumberOfPeople = 3,
                    PrivilegesList = new List<Privileges>
                    {
                        new Privileges
                        {
                            HotelRoomId = 1,
                            Id = 1,
                            Privilege = "Вид на море",
                        }
                    }

                }
                ,new HotelRoom
                {
                    Id = 2,
                    HotelId = 2,
                    Name= "Test 2",
                    Description = "Desc 2",
                    RoomPricePerNight = 78,
                }
                ,new HotelRoom
                {
                    HotelId = 1,
                    Id = 3,
                    Name= "Test 3",
                    Description = "Desc 3",
                    RoomPricePerNight = 98,
                },
            };



            var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.HotelRooms.AddRange(rooms);
				context.SaveChanges();

				var mockStringLocalizer = new Mock<IStringLocalizer<HotelRoomRepository>>();
				var movkPrivilegesReposirtory = new Mock<PrivilegesRepository>(context);
				var movkPhotoReposirtory = new Mock<IPhotoRoomsRepository>();
				var repository = new HotelRoomRepository(context, movkPhotoReposirtory.Object, movkPrivilegesReposirtory.Object);

				//Act
				var result = repository.GetAll(hotelId, NumberOfPeople);


				//Assert

				Assert.NotNull(result);
				Assert.Equal(1, result.Count());
				Assert.Equal(1, result.First().Id);
                Assert.NotNull(result.FirstOrDefault().Images);
                Assert.Equal(2, result.FirstOrDefault().Images.Count());
                Assert.NotNull(result.FirstOrDefault().PrivilegesList);
				Assert.Equal(1, result.FirstOrDefault().PrivilegesList.Count());


            }
		}
		[Fact]
		public void Add_ReturnsAddedHotelRoom()
		{
			// Arrange

			var formFileMock = new Mock<IFormFile>();
			var formFileContent = Encoding.UTF8.GetBytes("Test image data");
			var stream = new MemoryStream(formFileContent);
			formFileMock.Setup(f => f.OpenReadStream()).Returns(stream);
			formFileMock.Setup(f => f.FileName).Returns("test.jpg");
			formFileMock.Setup(f => f.Length).Returns(formFileContent.Length);

			var room = new HotelRoom
			{
				HotelId = 1,
				Id = 1,
				Name = "Test 1",
				Description = "Desc 1",
				RoomPricePerNight = 123,
				NumberOfPeople = 1,
				ImageModelList = new List<IFormFile>
				{
					formFileMock.Object
				},
                Privileges = new Privileges
                {
                    Id = 1,
                    HotelRoomId = 2,
                    Privilege = "Hello",
                }
            };

			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "BookingDatabase1")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();
				var mockPhotoStringLocalizer = new Mock<IStringLocalizer<PhotoRoomRepository>>();
				var mockPhotoRoomRepository = new Mock<PhotoRoomRepository>(context, mockPhotoStringLocalizer.Object);
                var movkPrivilegesReposirtory = new Mock<PrivilegesRepository>(context);

                var repository = new HotelRoomRepository(context, mockPhotoRoomRepository.Object, movkPrivilegesReposirtory.Object);

				// Act

				var result = repository.Add(room, room.HotelId);
				var addedRoomId = result.Id;
				var retrievedRoom = repository.Get(addedRoomId);
				var privilegesAdded = retrievedRoom.Privileges;

				// Assert

				Assert.NotNull(retrievedRoom);
				Assert.Equal(room.HotelId, retrievedRoom.HotelId);
				Assert.Equal(room.Name, retrievedRoom.Name);
				Assert.Equal(room.Description, retrievedRoom.Description);
				Assert.Equal(room.RoomPricePerNight, retrievedRoom.RoomPricePerNight);
				Assert.Equal(room.NumberOfPeople, retrievedRoom.NumberOfPeople);
				Assert.Single(retrievedRoom.Images);
                Assert.NotNull(result.PrivilegesList);
				Assert.Equal("Hello", privilegesAdded.Privilege);
            }
		}
		[Fact]
		public void Delete_RemoveHotelRoom()
		{
			// Arrange
			var roomId = 1;
			var rooms = new List<HotelRoom>
			{
				new HotelRoom
				{
					HotelId = 1,
					Id = 1,
					Name= "Test 1",
					Description = "Desc 1",
					RoomPricePerNight = 123,
					Images = new List<RoomImages>
					{
						new RoomImages
						{
							Id = 1,
							Name = "first",
							ImageData =  new byte[] { 0x22, 0x34, 0x76, 0x78 },
							RoomId = 1
						},

						new RoomImages
						{
							Id = 2,
							Name = "second",
							ImageData =  new byte[] { 0x22, 0x34, 0x76, 0x78 },
							RoomId = 1
						}

					}
				}
				,new HotelRoom
				{
					Id = 2,
					HotelId = 2,
					Name= "Test 2",
					Description = "Desc 2",
					RoomPricePerNight = 78,
				}
				,new HotelRoom
				{
					HotelId = 1,
					Id = 3,
					Name= "Test 3",
					Description = "Desc 3",
					RoomPricePerNight = 98,
				},
			};

			var options = new DbContextOptionsBuilder<BookingDbContext>()
					.UseInMemoryDatabase(databaseName: "TestDatabase2")
					.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.HotelRooms.AddRange(rooms);
				context.SaveChanges();
                var mockPhotoStringLocalizer = new Mock<IStringLocalizer<PhotoRoomRepository>>();
                var mockPhotoRoomRepository = new Mock<PhotoRoomRepository>(context, mockPhotoStringLocalizer.Object);
                var movkPrivilegesReposirtory = new Mock<PrivilegesRepository>(context);

                var repository = new HotelRoomRepository(context, mockPhotoRoomRepository.Object, movkPrivilegesReposirtory.Object);
                // Act
                repository.Delete(roomId);

				// Assert
				var remainingHotels = context.HotelRooms.ToList();
				var photos = context.RoomImages.Where(x => x.RoomId == roomId).ToList();
				var privileges = context.Privileges.Where(x => x.HotelRoomId == roomId);

				Assert.DoesNotContain(remainingHotels, h => h.Id == roomId);
				Assert.Empty(photos);
				Assert.Empty(privileges);

			}
		}
		[Fact]
		public void Update_UpdateHotelRoom()
		{
			// Arrange
			var room = new HotelRoom
			{
				Id = 2,
				HotelId = 2,
				Name = "Test 2",
				Description = "Desc 2",
				RoomPricePerNight = 78,
				Privileges = new Privileges
				{
					Id = 1,
					HotelRoomId = 2,
					Privilege = "Hello",
				}
			};
			var options = new DbContextOptionsBuilder<BookingDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			using (var context = new BookingDbContext(options))
			{
				context.Database.EnsureDeleted();

				context.HotelRooms.AddRange(room);
				context.SaveChanges();
                var mockPhotoStringLocalizer = new Mock<IStringLocalizer<PhotoRoomRepository>>();
                var mockPhotoRoomRepository = new Mock<PhotoRoomRepository>(context, mockPhotoStringLocalizer.Object);
                var movkPrivilegesReposirtory = new Mock<PrivilegesRepository>(context);

                var repository = new HotelRoomRepository(context, mockPhotoRoomRepository.Object, movkPrivilegesReposirtory.Object);

                // Act
                room.Name = "Updated Room Name";
				room.Privileges.Privilege = "Updated";
				var result = repository.Update(room);

				// Assert
				Assert.NotNull(result);
				Assert.Equal(room.Id, result.Id);
				Assert.Equal("Updated Room Name", result.Name);
				Assert.Equal("Updated", result.Privileges.Privilege);
			}
		}


	}
}
