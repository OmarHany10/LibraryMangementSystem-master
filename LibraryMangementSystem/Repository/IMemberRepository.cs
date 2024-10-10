using LibraryMangementSystem.Models;

namespace LibraryMangementSystem.Repository
{
    public interface IMemberRepository
    {
        public void Add(Member member);
        public void Delete(int id);
        public void Update(Member member);
        public Member GetByID(int id);
        public List<Member> GetAll();

        public void Save();
    }
}
