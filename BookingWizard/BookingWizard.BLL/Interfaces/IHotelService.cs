﻿
using BookingWizard.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWizard.BLL.Interfaces
{
	public interface IHotelService
	{
		public Hotel Add(Hotel item);
		public void Delete(Hotel item);
		public Hotel Update(Hotel item);
		public Hotel Get(int id);
		IEnumerable<Hotel> GetAll(string name = "");
		public void  DeletePhoto(string photoName);
		public void  PhotoUpload(Hotel hotel);
	}
}
