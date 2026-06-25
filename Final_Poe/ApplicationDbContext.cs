using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CyberSecurityAwarenessBot
{
    public class ApplicationDbContext : DbContext
    {
        // DbSet represents the table in the database
        public DbSet<TaskItem> Tasks { get; set; }

        // Configure the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using SQLite database
            // The database file will be created in the application folder
            optionsBuilder.UseSqlite("Data Source=cybersecuritybot.db");

            // Enable logging to see SQL queries (optional - for debugging)
            // optionsBuilder.LogTo(Console.WriteLine);
        }

        // Configure entity mappings and relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TaskItem entity
            modelBuilder.Entity<TaskItem>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.Id);

                // Property configurations
                entity.Property(e => e.Title)
                    .IsRequired()  // NOT NULL
                    .HasMaxLength(255);  // VARCHAR(255)

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);  // VARCHAR(1000)

                entity.Property(e => e.Reminder)
                    .HasMaxLength(100);  // VARCHAR(100)

                // Default values
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsCompleted)
                    .HasDefaultValue(false);

                // Indexes for better performance
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.IsCompleted);
            });
        }
    }
}