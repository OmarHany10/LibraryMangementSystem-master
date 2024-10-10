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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int bookId, int memberId, DateTime dueDate)
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
            return View();
        }

        // GET: List all checkouts..
        public async Task<IActionResult> Index()
        {
            var checkouts = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Member)
                .ToListAsync();

            return View(checkouts);
        }
    }
}

