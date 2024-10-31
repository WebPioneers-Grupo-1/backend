using Microsoft.EntityFrameworkCore;
using TakeMyCar.Services.Domain.Model.Aggregates;

namespace TakeMyCar.Services.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Rental>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);
            
        }
    }
}
