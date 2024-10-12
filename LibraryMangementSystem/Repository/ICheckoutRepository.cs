using LibraryMangementSystem.Models;

namespace LibraryMangementSystem.Repository
{
    public interface ICheckoutRepository
    {
        public void Add(Checkout checkout);
        public void Delete(int id);
        public void Update(Checkout checkout);
        public Checkout GetByID(int id);
        public List<Checkout> GetAll();

        public List<Checkout> GetByMemberID(int id);

        public void Save();
    }
}
