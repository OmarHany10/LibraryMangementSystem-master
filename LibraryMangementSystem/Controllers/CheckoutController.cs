using LibraryMangementSystem.Data;
using LibraryMangementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryMangementSystem.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;

        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewData["Members"] = _context.Members.ToList();
            ViewData["Books"] = _context.Books.Where(b => b.AvailableCopies > 0).ToList(); //available books
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCreate(int bookId, int memberId, DateTime dueDate)
        {
            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(bookId);

                // Check if the book is valid and available
                if (book == null || book.AvailableCopies == 0)
                {
                    return NotFound("Book not found or unavailable.");
                }

                var checkout = new Checkout
                {
                    BookId = bookId,
                    MemberId = memberId,
                    CheckoutDate = DateTime.Now,
                    DueDate = dueDate,
                    ReturnDate = null,
                    Penalty = null
                };

                // Mark the book as checked out
                if (book.AvailableCopies > 0)
                {
                    book.AvailableCopies -= 1;
                }
                else
                {
                    return NotFound("Book unavailable.");
                }

                _context.Add(checkout);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index)); // Redirect after successful checkout
            }

            // If invalid, reload view with existing data
            ViewData["Members"] = _context.Members.ToList();
            ViewData["Books"] = _context.Books.Where(b => b.AvailableCopies > 0).ToList();
            return View("Create");
        }

        // GET: List all checkouts..
        public async Task<IActionResult> Index()
        {
            var checkouts = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Member).Where(c => c.ReturnDate == null)
                .ToListAsync();

            return View("Index", checkouts);
        }

        public async Task<IActionResult> Return(int CheckoutId)
        {
            var checkout = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Penalty)
                .FirstOrDefaultAsync(c => c.CheckoutId == CheckoutId);

            if (checkout != null)
            {
                var returnEntry = new Return
                {
                    ReturnDate = DateTime.Now,
                    CheckoutId = CheckoutId
                };

                checkout.ReturnDate = returnEntry.ReturnDate;
                checkout.Book.AvailableCopies += 1; // Increase available copies

                // Calculate penalty based on late days
                var lateDays = (returnEntry.ReturnDate - checkout.DueDate).Days;
                if (lateDays > 0)
                {
                    returnEntry.LateDays = lateDays;
                    returnEntry.PenaltyAmount = lateDays * 1; // $1 penalty per late day
                }
                else
                {
                    returnEntry.LateDays = 0;
                    returnEntry.PenaltyAmount = 0;
                }

                _context.Returns.Add(returnEntry);
                _context.Update(checkout.Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

