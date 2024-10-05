using LibraryMangementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMangementSystem.Data.config
{
    public class PenaltyConfiguration : IEntityTypeConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.HasKey(p=>p.PenaltyId);
        }
    }
}
