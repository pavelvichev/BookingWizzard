﻿using System.ComponentModel.DataAnnotations;

namespace BookingWizard.ModelsVM
{
	public class HotelVM
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Field can`t be null")]
		public string HotelName { get; set; } // навзание отеля
		[Required(ErrorMessage = "Field can`t be null")]
		public string HotelShortDescription { get; set; } // короткое описание отеля(на карточке)
		[Required(ErrorMessage = "Field can`t be null")]
		public string HotelLongDescription { get; set; } // общее описание (при нажатии)

		public ushort HotelMark { get; set; } // оценка
		public bool isFavourite { get; set; } // добавить в избраное

		public List<string> previlege; //привилегии
		public AddressVM address { get; set; } // Аддрес
		public int addressId { get; set; } // айди адресса в таблице
		public IEnumerable<IFormFile> ImageModelList { get; set; }
		public HotelImagesVM? Image { get; set; }
		public IEnumerable<HotelImagesVM>? Images { get; set; }
		
		public hotelRoomVM? room { get; set; }

		public IEnumerable<hotelRoomVM>? roomList { get; set; }

		//вавываываыв.аэ



	}
}
