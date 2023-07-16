using BookingWizard.DAL.Entities.HotelRooms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.DAL.Entities.Hotels
{
    public class Hotel
    {
        public int Id { get; set; }

        public string HotelName { get; set; } 

        public string HotelShortDescription { get; set; } 

        public string HotelLongDescription { get; set; } 

        public ushort HotelMark { get; set; } 

        public IEnumerable<string> previlege; 
        public Address Address { get; set; } 
        [NotMapped]
        public IEnumerable<IFormFile> ImageModelList { get; set; }
        [NotMapped]
        public HotelImages Image { get; set; }
        public ICollection<HotelImages> Images { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public IEnumerable<HotelRoom>? roomList { get; set; }

        public string IdentityUserId { get; set; }


    }
}
