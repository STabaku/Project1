using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tables in your database
        public DbSet<User> Users { get; set; }
        public DbSet<EmergencyRequest> EmergencyRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Pharmacy is keyless (not stored in DB)
            modelBuilder.Entity<Pharmacy>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
