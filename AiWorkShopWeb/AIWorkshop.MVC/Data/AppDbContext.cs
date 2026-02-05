using AIWorkshop.MVC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkshop.MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<PromptScore> PromptScores => Set<PromptScore>();
        public DbSet<QuizScore> QuizScores => Set<QuizScore>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(200);

            modelBuilder.Entity<PromptScore>()
                .HasOne(p => p.User)
                .WithMany(u => u.PromptScores)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<QuizScore>()
                .HasOne(q => q.User)
                .WithMany(u => u.QuizScores)
                .HasForeignKey(q => q.UserId);
        }
    }
}
