namespace BookingWizard.ModelsVM
{
    public class BookingVM
    {
        public int Id { get; set; }
		public int roomId { get; set; }
		public string RoomName { get; set; }
		public string HotelName { get; set; }
        public uint allPrice { get; set; }
		public DateTime ArrivalDate { get; set; } = DateTime.Now;
        public DateTime DateOfDeparture { get; set; } = DateTime.Now;

        public string FirstNameBuyer { get; set; }
        public string LastNameBuyer { get; set; }
        public string Email { get; set; }

        public string IdentityUserId { get; set; }
     }
}
