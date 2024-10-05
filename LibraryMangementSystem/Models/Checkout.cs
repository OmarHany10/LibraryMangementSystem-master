namespace LibraryMangementSystem.Models
{
    public class Checkout
    {
        public int CheckoutId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public Penalty Penalty { get; set; }
        public Return Return { get; set; }
    }    
}
