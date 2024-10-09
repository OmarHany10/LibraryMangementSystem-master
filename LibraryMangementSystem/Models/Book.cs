namespace LibraryMangementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int AvailableCopies { get; set; }

        public ICollection<Checkout>? Checkouts { get; set; }
    }    
}
