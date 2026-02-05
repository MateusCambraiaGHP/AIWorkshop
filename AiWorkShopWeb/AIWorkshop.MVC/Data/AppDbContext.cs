using AIWorkshop.MVC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkshop.MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<PromptScore> PromptScores => Set<PromptScore>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<PromptScore>()
                .HasOne(p => p.User)
                .WithMany(u => u.PromptScores)
                .HasForeignKey(p => p.UserId);
        }
    }
}
