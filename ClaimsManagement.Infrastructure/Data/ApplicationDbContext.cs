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

        public DbSet<Insured> Insureds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Insured)
                .WithMany(i => i.Claims)
                .HasForeignKey(c => c.InsuredId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
