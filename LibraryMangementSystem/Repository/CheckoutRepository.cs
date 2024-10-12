using LibraryMangementSystem.Data;
using LibraryMangementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMangementSystem.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly AppDbContext context;

        public CheckoutRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Checkout checkout)
        {
            context.Checkouts.Add(checkout);
        }

        public void Delete(int id)
        {
            Checkout checkout = GetByID(id);
            context.Checkouts.Remove(checkout);
        }

        public List<Checkout> GetAll()
        {
            throw new NotImplementedException();
        }

        public Checkout GetByID(int id)
        {
            return context.Checkouts.FirstOrDefault(ch => ch.CheckoutId == id);
        }

        public List<Checkout> GetByMemberID(int id)
        {
            List<Checkout> checkouts = context.Checkouts.Include(ch => ch.Book).Include(ch => ch.Penalty).Where(ch => ch.MemberId == id).ToList();
            return checkouts;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Checkout checkout)
        {
            context.Checkouts.Update(checkout);
        }
    }
}
