using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.Core.Entities
{
	public class Hotel
	{
		public int Id { get; set; }
	
		public string HotelName { get; set; } // навзание отеля
	
		public string HotelShortDescription { get; set; } // короткое описание отеля(на карточке)
		
		public string HotelLongDescription { get; set; } // общее описание (при нажатии)

		public ushort HotelMark { get; set; } // оценка
		public bool isFavourite { get; set; } // добавить в избраное

		public List<string> previlege; //привилегии
		public Address address { get; set; } // Аддрес


		public int addressId { get; set; } // айди адресса в таблице
		public string imageUrl { get; set; } // фото

		public hotelRoom? room { get; set; }

		public IEnumerable<hotelRoom>? roomList { get; set; }




	}
}
