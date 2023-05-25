namespace BookingWizard.Models
{
    public class BookingDTO
    {
        public int Id { get; set; }

		public int roomId { get; set; }

		public DateTime arrival_date { get; set; }
        public DateTime date_of_departure { get; set; }
     }
}
