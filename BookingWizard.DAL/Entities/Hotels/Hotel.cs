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

        public string HotelName { get; set; } // навзание отеля

        public string HotelShortDescription { get; set; } // короткое описание отеля(на карточке)

        public string HotelLongDescription { get; set; } // общее описание (при нажатии)

        public ushort HotelMark { get; set; } // оценка
        public bool isFavourite { get; set; } // добавить в избраное

        public IEnumerable<string> previlege; //привилегии
        public Address Address { get; set; } // Аддрес
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
