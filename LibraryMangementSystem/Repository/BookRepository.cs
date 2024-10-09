using LibraryMangementSystem.Data;
using LibraryMangementSystem.Models;

namespace LibraryMangementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext context;

        public BookRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Book book)
        {
            context.Add(book);
        }

        public void Delete(int id)
        {
            Book book = GetById(id);
            context.Remove(book);
        }

        public List<Book> GetAll()
        {
            return context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return context.Books.FirstOrDefault(b => b.BookId == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Book book)
        {
            context.Update(book);
        }
    }
}
