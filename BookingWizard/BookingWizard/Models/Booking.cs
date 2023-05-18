namespace BookingWizard.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Hotel hotel { get; set; }

        public DateTime arrival_date { get; set; }
        public DateTime date_of_departure { get; set; }
     }
}
