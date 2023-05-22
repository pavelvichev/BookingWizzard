namespace BookingWizard.Models
{
    public class BookingDTO
    {
        public int Id { get; set; }
        
        public hotelRoomDTO room { get; set; }

        public DateTime arrival_date { get; set; }
        public DateTime date_of_departure { get; set; }
     }
}
