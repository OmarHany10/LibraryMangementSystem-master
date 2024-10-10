using LibraryMangementSystem.Data;
using LibraryMangementSystem.Models;
using LibraryMangementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryMangementSystem.Controllers
{
    public class ReturnController : Controller
    {
        private readonly AppDbContext _context;

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Return(int checkoutId)
        {
            var checkout = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Penalty)
                .FirstOrDefaultAsync(c => c.CheckoutId == checkoutId);

            if (checkout != null && checkout.ReturnDate == null)
            {
                var returnEntry = new Return
                {
                    ReturnDate = DateTime.Now,
                    CheckoutId = checkoutId
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
