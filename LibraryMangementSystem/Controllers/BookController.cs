using LibraryMangementSystem.Models;
using LibraryMangementSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMangementSystem.Controllers
{
    [Authorize(Roles = "Admin, Librarians")]
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

        public IActionResult Search(string searchString)
        {
            List<Book> books = bookRepository.GetAll(); // Get all books first

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
