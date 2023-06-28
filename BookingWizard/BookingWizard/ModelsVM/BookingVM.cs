namespace BookingWizard.ModelsVM
{
    public class BookingVM
    {
        public int Id { get; set; }

		public int roomId { get; set; }

        public uint allPrice { get; set; }
		public DateTime arrival_date { get; set; } = DateTime.Now;
        public DateTime date_of_departure { get; set; } = DateTime.Now;
     }
}
