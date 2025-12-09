using Microsoft.EntityFrameworkCore;
using UDEApp.Shared.Models;

namespace UDEApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses => Set<Course>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FullContent ve ShortDescription için sınırsız TEXT
            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.FullContent)
                      .HasColumnType("TEXT");

                entity.Property(e => e.ShortDescription)
                      .HasColumnType("TEXT");
            });
        }
    }
}