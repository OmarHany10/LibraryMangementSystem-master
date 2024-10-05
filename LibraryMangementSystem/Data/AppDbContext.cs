using LibraryMangementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryMangementSystem.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Return> Returns { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var constr = configuration.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(constr);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        //    // Add other configurations here...
        //}
    }
}
