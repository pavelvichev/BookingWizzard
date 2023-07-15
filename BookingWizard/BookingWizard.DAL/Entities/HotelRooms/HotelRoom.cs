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
        public string Name { get; set; } // имя номера

        public string Description { get; set; } // описание номера

        public Hotel? Hotel { get; set; }// инфо про отель
        public int HotelId { get; set; }

        public ushort RoomPricePerNight { get; set; } // цена за ночь

        public int NumberOfPeople { get; set; }


        public ICollection<Booking>? Bookings { get; set; } // информация про бронь

        [NotMapped]
        public IEnumerable<IFormFile> ImageModelList { get; set; }
        [NotMapped]
        public RoomImages Image { get; set; }
        public ICollection<RoomImages> Images { get; set; }


        public bool isBooking { get; set; } // занят ли уже номер
    }
}
