using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // TABLES
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }   
        public DbSet<EmergencyRequest> EmergencyRequests { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Pharmacy soft delete
            modelBuilder.Entity<Pharmacy>()
                .HasQueryFilter(p => p.IsActive);

            // 🔥 RELACIONI ORDER → ORDERITEMS
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
