using LibraryMangementSystem.Data;
using LibraryMangementSystem.Models;

namespace LibraryMangementSystem.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext context;

        public MemberRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Member member)
        {
            context.Members.Add(member);
        }

        public void Delete(int id)
        {
            Member member = GetByID(id);
            context.Members.Remove(member);
        }

        public List<Member> GetAll()
        {
            return context.Members.ToList();
        }

        public Member GetByID(int id)
        {
            return context.Members.FirstOrDefault(m => m.MemberId == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Member member)
        {
            context.Update(member);
        }
    }
}
