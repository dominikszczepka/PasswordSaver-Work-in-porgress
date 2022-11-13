using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace PassSaver.Entities
{
    public class PassSaverDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\Local;Database=PasswordDb;Trusted_Connection=True;";
        public DbSet<Password> Passwords { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Password>()
                .Property(p=>p.S_Password)
                .IsRequired()
                .HasMaxLength(64);

            modelBuilder.Entity<Password>()
                .Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(128);

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(32);

            modelBuilder.Entity<User>()
                .Property(u => u.UserHashedPassword)
                .IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
