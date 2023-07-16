using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookingWizard.DAL.Entities.Hotels;

namespace BookingWizard.DAL.Entities.HotelRooms
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public Hotel? Hotel { get; set; }
        public int HotelId { get; set; }
        public ushort RoomPricePerNight { get; set; }
        public int NumberOfPeople { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        [NotMapped]
        public IEnumerable<IFormFile> ImageModelList { get; set; }
        [NotMapped]
        public RoomImages Image { get; set; }
        public ICollection<RoomImages> Images { get; set; }
        public bool isBooking { get; set; }
        [NotMapped]
        public Privileges Privileges { get; set; }
        public List<Privileges> PrivilegesList { get; set; }

	}
}
