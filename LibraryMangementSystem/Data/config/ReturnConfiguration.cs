using LibraryMangementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMangementSystem.Data.config
{
    public class ReturnConfiguration : IEntityTypeConfiguration<Return>
    {
        public void Configure(EntityTypeBuilder<Return> builder)
        {
            builder.HasKey(r => r.ReturnId);
        }
    }
}
