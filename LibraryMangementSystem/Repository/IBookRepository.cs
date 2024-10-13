using LibraryMangementSystem.Models;

namespace LibraryMangementSystem.Repository
{
    public interface IBookRepository
    {
        public void Add(Book book);
        public void Update(Book book);
        public void Delete(int id);

        public Book GetById(int id);
        public List<Book> GetAll();

        public List<Book> GetAllAvialable();

        public void Save();

    }
}
