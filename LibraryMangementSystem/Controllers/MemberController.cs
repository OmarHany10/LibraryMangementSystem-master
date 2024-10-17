using LibraryMangementSystem.Models;
using LibraryMangementSystem.Repository;
using LibraryMangementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;

namespace LibraryMangementSystem.Controllers
{
    [Authorize(Roles = "Members")]
    public class MemberController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemberRepository memberRepository;
        private readonly ICheckoutRepository checkoutRepository;
        private readonly IBookRepository bookRepository;

        public MemberController(UserManager<ApplicationUser> userManager, IMemberRepository memberRepository, ICheckoutRepository checkoutRepository, IBookRepository bookRepository)
        {
            this.userManager = userManager;
            this.memberRepository = memberRepository;
            this.checkoutRepository = checkoutRepository;
            this.bookRepository = bookRepository;
        }
        public IActionResult MemberInformation()
        {
            string email = User.Identity.Name;
            Member member = memberRepository.GetByEmail(email);
            MemberInformationViewModel memberInformation = new MemberInformationViewModel()
            {
                Address = member.Address,
                DateOfBirth = member.DateOfBirth,
                Email = email,
                FirstName = member.FirstName,
                LastName = member.LastName,
                PhoneNumber = member.PhoneNumber,
            };
            return View("MemberInformation", memberInformation);
        }

        public IActionResult BorrowingHistory()
        {
            string email = User.Identity.Name;
            Member member = memberRepository.GetByEmail(email);
            List<Checkout> checkouts = checkoutRepository.GetByMemberID(member.MemberId);
            List<BorrowingHistoryViewModel> borrowingHistories = new List<BorrowingHistoryViewModel>();
            foreach (var checkout in checkouts)
            {
                BorrowingHistoryViewModel borrowingHistory = new BorrowingHistoryViewModel();
                if (checkout.ReturnDate != null)
                {
                    borrowingHistory.BookName = checkout.Book.Title;
                    borrowingHistory.CheckoutDate = checkout.CheckoutDate;
                    borrowingHistory.DueDate = checkout.DueDate;
                    borrowingHistory.ReturnDate = checkout.ReturnDate;
                    borrowingHistory.PenalityAmount = checkout?.Penalty?.PenaltyAmount ?? 0;
                    borrowingHistories.Add(borrowingHistory);
                }
            }
            return View("BorrowingHistory", borrowingHistories);
        }

        public IActionResult MyBorrowedBook()
        {
            string email = User.Identity.Name;
            Member member = memberRepository.GetByEmail(email);
            List<Checkout> checkouts = checkoutRepository.GetByMemberID(member.MemberId);
            List<BorrowingHistoryViewModel> borrowingHistories = new List<BorrowingHistoryViewModel>();
            foreach (var checkout in checkouts)
            {
                BorrowingHistoryViewModel borrowingHistory = new BorrowingHistoryViewModel();
                if (checkout?.ReturnDate == null)
                {
                    borrowingHistory.BookName = checkout.Book.Title;
                    borrowingHistory.CheckoutDate = checkout.CheckoutDate;
                    borrowingHistory.DueDate = checkout.DueDate;
                    borrowingHistory.ReturnDate = checkout.ReturnDate;

                    // Create a new Penalty object if it doesn't exist
                    Penalty penalty = new Penalty
                    {
                        LateDaysThreshold = 0, // Set your desired threshold here (in days)
                        PenaltyAmount = 2, // Default penalty per day (for example, $2 per day)
                        CheckoutId = checkout.CheckoutId
                    };
                    var lateMinutes = (DateTime.Now - checkout.DueDate).TotalMinutes;
                    // Apply penalty based on minutes
                    if (lateMinutes > penalty.LateDaysThreshold * 1440) // Threshold converted to minutes
                    {
                        var minutesWithPenalty = (decimal)(lateMinutes - (penalty.LateDaysThreshold * 1440)); // Threshold converted to minutes
                        penalty.PenaltyAmount = minutesWithPenalty * (penalty.PenaltyAmount); // Penalty per minute
                    }
                    else
                    {
                        penalty.PenaltyAmount = 0; // No penalty if within the threshold
                    }
                    borrowingHistory.PenalityAmount = penalty.PenaltyAmount;
                    borrowingHistories.Add(borrowingHistory);
                }
            }
            return View("MyBorrowedBook", borrowingHistories);
        }

        public IActionResult BorrowABook()
        {
            List<Book> books = bookRepository.GetAllAvialable();
            return View("BorrowABook", books);
        }

        
        public IActionResult BorrowDetails(int BookId)
        {
            Book book = bookRepository.GetById(BookId);
            return View("BorrowDetails",book);
        }

        [HttpPost]
        public IActionResult BorrowBook(int BookId, DateTime dueDate)
        {
            Book book = bookRepository.GetById(BookId);
            string email = User.Identity.Name;
            Member member = memberRepository.GetByEmail(email);

            var checkout = new Checkout
            {
                BookId = book.BookId,
                MemberId = member.MemberId,
                CheckoutDate = DateTime.Now,
                DueDate = dueDate,
                ReturnDate = null,
                Penalty = null
            };
            book.AvailableCopies -= 1;
            checkoutRepository.Add(checkout);
            checkoutRepository.Save();

            return RedirectToAction("MyBorrowedBook");

        }

        public IActionResult Search(string searchString)
        {
            List<Book> books = bookRepository.GetAllAvialable(); // Get all books first

            if (!string.IsNullOrEmpty(searchString))
            {
                // Filter books by title, author, or genre
                books = books.Where(book =>
                    book.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    book.Author.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    book.Genre.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            return View("Search", books);
        }
    }
}
