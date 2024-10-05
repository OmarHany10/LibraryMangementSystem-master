using LibraryMangementSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryMangementSystem.Data.config
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.MemberId);

            // Member -> Checkout (One to Many)
            builder.HasMany(c => c.Checkouts)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId);
        }
    }
}
