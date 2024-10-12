using LibraryMangementSystem.Models;
using LibraryMangementSystem.Repository;
using LibraryMangementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace LibraryMangementSystem.Controllers
{
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
    }
}
