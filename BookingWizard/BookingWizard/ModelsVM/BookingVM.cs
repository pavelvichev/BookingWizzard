namespace BookingWizard.ModelsVM
{
    public class BookingVM
    {
        public uint Id { get; set; }

		public uint roomId { get; set; }

		public DateTime arrival_date { get; set; }
        public DateTime date_of_departure { get; set; }
     }
}
