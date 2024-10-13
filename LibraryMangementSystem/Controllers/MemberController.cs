using LibraryMangementSystem.Models;
using LibraryMangementSystem.Repository;
using LibraryMangementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace LibraryMangementSystem.Controllers
{
    [Authorize(Roles = "Members")]
    public class MemberController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemberRepository memberRepository;
        private readonly ICheckoutRepository checkoutRepository;

        public MemberController(UserManager<ApplicationUser> userManager, IMemberRepository memberRepository, ICheckoutRepository checkoutRepository)
        {
            this.userManager = userManager;
            this.memberRepository = memberRepository;
            this.checkoutRepository = checkoutRepository;
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
                if (checkout?.Penalty?.PenaltyAmount != null)
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

        

    }
}
