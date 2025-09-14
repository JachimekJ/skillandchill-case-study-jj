using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<LoginAttempts> LoginAttempts { get; set; }
        public DbSet<ActivityLogs> ActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacja: jeden Role -> wielu Users
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Relacja: Users -> LoginAttempts
            modelBuilder.Entity<LoginAttempts>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);

            // Relacja: Users -> ActivityLogs
            modelBuilder.Entity<ActivityLogs>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            // --- SEED ROLES ---
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "Employee", Description = "Ograniczony dostęp" },
                new Roles { Id = 2, Name = "Distributor", Description = "Dostęp do własnych danych" },
                new Roles { Id = 3, Name = "ExportManager", Description = "Zarządza danymi dystrybutorów" },
                new Roles { Id = 4, Name = "Admin", Description = "Pełny dostęp do danych" },
                new Roles { Id = 5, Name = "SuperAdmin", Description = "Wszystkie uprawnienia" }
            );

            // --- SEED ADMIN USER ---
            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    PasswordHash = "admin123", // TODO: zmienić na hash
                    RoleId = 4,
                    IsLocked = false,
                    CreatedAt = new DateTime(2025, 1, 1),
                    UpdatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}