using LibraryMangementSystem.Models;
using LibraryMangementSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMangementSystem.Controllers
{
    [Authorize(Roles = "Librarians")]
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public IActionResult Index()
        {
            List<Book> books = bookRepository.GetAll();
            return View("Index", books);
        }

        public IActionResult Details(int id)
        {
            Book book = bookRepository.GetById(id);
            return View("Details",book);
        }

        public IActionResult Add()
        {
            return View("Add");
        }

        public IActionResult SaveAdd(Book book)
        {
            if(ModelState.IsValid)
            {
                bookRepository.Add(book);
                bookRepository.Save();
                return RedirectToAction("Index");
            }
            return View("Add", book);
        }
    }
}
