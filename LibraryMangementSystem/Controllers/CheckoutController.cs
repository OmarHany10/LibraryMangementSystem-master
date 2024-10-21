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

                // Calculate the number of late days
                var lateDays = (returnEntry.ReturnDate - checkout.DueDate).Days;

                // Initialize penalty amount
                decimal penaltyAmount = 0;
                Penalty penalty = null;

                if (lateDays > 0)
                {
                    returnEntry.LateDays = lateDays;

                    // If there's already a Penalty object for this checkout, update it
                    if (checkout.Penalty != null)
                    {
                        penalty = checkout.Penalty;

                        // Apply penalty only if late days exceed the threshold
                        if (lateDays > penalty.LateDaysThreshold)
                        {
                            var daysWithPenalty = lateDays - penalty.LateDaysThreshold;
                            penaltyAmount = daysWithPenalty * penalty.PenaltyAmount;
                        }
                        else
                        {
                            penaltyAmount = 0; // No penalty if within the threshold
                        }

                        // Update the penalty object
                        penalty.PenaltyAmount = penaltyAmount;
                    }
                    else
                    {
                        // Create a new Penalty object if it doesn't exist
                        penalty = new Penalty
                        {
                            LateDaysThreshold = 1, // Set your desired threshold here
                            PenaltyAmount = 2, // Default penalty per day (for example, $2 per day)
                            CheckoutId = checkout.CheckoutId
                        };

                        if (lateDays > penalty.LateDaysThreshold)
                        {
                            var daysWithPenalty = lateDays - penalty.LateDaysThreshold;
                            penalty.PenaltyAmount = daysWithPenalty * penalty.PenaltyAmount;
                        }
                        else
                        {
                            penalty.PenaltyAmount = 0; // No penalty if within the threshold
                        }

                        // Add new penalty record
                        _context.Penalties.Add(penalty);
                    }

                    returnEntry.PenaltyAmount = penalty.PenaltyAmount;
                }
                else
                {
                    // No late days, no penalty
                    returnEntry.LateDays = 0;
                    returnEntry.PenaltyAmount = 0;
                }



                ////////////////////////////////////////////////////////////////////////////////////////////////////
                #region CalculatePenalityWithMinutes

                //// Calculate the number of late minutes
                //var lateMinutes = (returnEntry.ReturnDate - checkout.DueDate).TotalMinutes;

                //// Initialize penalty amount
                //decimal penaltyAmount = 0;
                //Penalty penalty = null;

                //if (lateMinutes > 0)
                //{
                //    returnEntry.LateDays = (int)Math.Floor(lateMinutes / 1440); // Convert minutes to days for logging purposes (optional)

                //    // If there's already a Penalty object for this checkout, update it
                //    if (checkout.Penalty != null)
                //    {
                //        penalty = checkout.Penalty;

                //        // Apply penalty based on minutes
                //        if (lateMinutes > penalty.LateDaysThreshold * 1440) // Convert LateDaysThreshold to minutes
                //        {
                //            var minutesWithPenalty = (decimal)(lateMinutes - (penalty.LateDaysThreshold * 1440)); // Threshold converted to minutes
                //            penaltyAmount = minutesWithPenalty * (penalty.PenaltyAmount); // Apply penalty per minute based on daily rate
                //        }
                //        else
                //        {
                //            penaltyAmount = 0; // No penalty if within the threshold
                //        }

                //        // Update the penalty object
                //        penalty.PenaltyAmount = penaltyAmount;
                //    }
                //    else
                //    {
                //        // Create a new Penalty object if it doesn't exist
                //        penalty = new Penalty
                //        {
                //            LateDaysThreshold = 0, // Set your desired threshold here (in days)
                //            PenaltyAmount = 2, // Default penalty per day (for example, $2 per day)
                //            CheckoutId = checkout.CheckoutId
                //        };

                //        // Apply penalty based on minutes
                //        if (lateMinutes > penalty.LateDaysThreshold * 1440) // Threshold converted to minutes
                //        {
                //            var minutesWithPenalty = (decimal)(lateMinutes - (penalty.LateDaysThreshold * 1440)); // Threshold converted to minutes
                //            penalty.PenaltyAmount = minutesWithPenalty * (penalty.PenaltyAmount); // Penalty per minute
                //        }
                //        else
                //        {
                //            penalty.PenaltyAmount = 0; // No penalty if within the threshold
                //        }

                //        // Add new penalty record
                //        _context.Penalties.Add(penalty);
                //    }

                //    returnEntry.PenaltyAmount = penalty.PenaltyAmount;
                //}
                //else
                //{
                //    // No late minutes, no penalty
                //    returnEntry.LateDays = 0;
                //    returnEntry.PenaltyAmount = 0;
                //}
                #endregion


                _context.Returns.Add(returnEntry);
                _context.Update(checkout.Book);
                _context.Update(checkout);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: List all Penalty..
        public async Task<IActionResult> Penalty()
        {
            var penalty = _context.Penalties
                .Include(c => c.Checkout)
                .Include(m => m.Checkout.Member)
                .ToList();

            return View("penalty", penalty);
        }
    }
}

