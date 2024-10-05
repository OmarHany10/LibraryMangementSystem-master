using LibraryMangementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMangementSystem.Data.config
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.BookId);


            // Book -> Checkout (One to Many)
            builder.HasMany(c => c.Checkouts)
                .WithOne(b => b.Book)
                .HasForeignKey(b => b.BookId);
        }
    }
}
