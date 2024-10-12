using System.ComponentModel.DataAnnotations;

namespace LibraryMangementSystem.ViewModels
{
    public class BorrowingHistoryViewModel
    {
        [Display(Name = "Book Name")]
        public string? BookName { get; set; }

        [Display(Name ="Checkout Date")]
        public DateTime? CheckoutDate { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Penality Amount")]
        public decimal? PenalityAmount { get; set; }

    }
}
