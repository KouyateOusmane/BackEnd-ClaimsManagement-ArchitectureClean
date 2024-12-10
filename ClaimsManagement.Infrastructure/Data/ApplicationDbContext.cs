using ClaimsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClaimsManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration exemple
            modelBuilder.Entity<Claim>().Property(c => c.ClaimType).IsRequired().HasMaxLength(50);
        }
    }
}
