namespace LibraryMangementSystem.Models
{
    public class Return
    {
        public int ReturnId { get; set; }
        public DateTime ReturnDate { get; set; }
        public int LateDays { get; set; }
        public decimal PenaltyAmount { get; set; }

        public int CheckoutId { get; set; }
        public Checkout Checkout { get; set; }
    }
}
