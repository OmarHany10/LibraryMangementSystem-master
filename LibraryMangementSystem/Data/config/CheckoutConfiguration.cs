using LibraryMangementSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryMangementSystem.Data.config
{
    public class CheckoutConfiguration : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            builder.HasKey(x => x.CheckoutId);


            // Checkout -> Return (One to One)
            builder.HasOne(r => r.Return)
                .WithOne(c => c.Checkout)
                .HasForeignKey<Return>(r => r.CheckoutId);

            // Checkout -> Penalty (One to One)
            builder.HasOne(p => p.Penalty)
                .WithOne(c => c.Checkout)
                .HasForeignKey<Penalty>(c => c.CheckoutId);

        }
    }
}
