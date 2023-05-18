namespace BookingWizard.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string imageUrl { get; set; }
        public string HotelName { get; set; }
        public string HotelShortDescription { get; set; }
        public string HotelLongDescription { get; set; }
        public ushort HotelPricePerNight { get; set; }
        public ushort HotelMark { get; set; }
        public bool isFavourite { get; set; }

        public List<string> previlege;
        public Address address { get; set; }
        public int AddresId { get; set; }
        public Booking bookingInfo { get; set; }
        public int bookingId { get; set; }


    }
}
