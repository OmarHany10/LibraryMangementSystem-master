namespace LibraryMangementSystem.Models
{
    public class Penalty
    {
        public int PenaltyId { get; set; }
        public int LateDaysThreshold { get; set; }
        public decimal PenaltyAmount { get; set; }

        public int CheckoutId { get; set; }
        public Checkout Checkout { get; set; }
    }
}
