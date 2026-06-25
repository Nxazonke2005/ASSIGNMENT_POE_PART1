using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CyberSecurityAwarenessBot
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cybersecuritybot.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.Property(e => e.Reminder)
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsCompleted)
                    .HasDefaultValue(false);

                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.IsCompleted);
            });
        }
    }
}