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
                    PasswordHash = "$2a$11$l.xBhLrd5QUl0IjgnVFOu.fPwPe9rSjKfYnowuvZ5.n9bysAVS4Ru", // admin123
                    RoleId = 4,
                    IsLocked = false,
                    CreatedAt = new DateTime(2025, 1, 1),
                    UpdatedAt = new DateTime(2025, 1, 1)
                }
            );

            modelBuilder.Entity<MediaFile>().HasData(
                new MediaFile { Id = 1, Name = "SKU123_main_1.jpg", Type = "image", Size = 512, Date = new DateTime(2025, 3, 1), Url = "/media/SKU123_main_1.jpg" },
                new MediaFile { Id = 2, Name = "SKU123_manual.pdf", Type = "pdf", Size = 120, Date = new DateTime(2025, 3, 1), Url = "/media/SKU123_manual.pdf" },
                new MediaFile { Id = 3, Name = "spring_sale_SKU123_SKU222.jpg", Type = "image", Size = 1024, Date = new DateTime(2025, 3, 10), Url = "/media/spring_sale_SKU123_SKU222.jpg" },
                new MediaFile { Id = 4, Name = "SKU222_ingredients.pdf", Type = "pdf", Size = 90, Date = new DateTime(2025, 2, 15), Url = "/media/SKU222_ingredients.pdf" }
            );


        }
        public DbSet<SalesReports> SalesReports { get; set; }

        public DbSet<PurchaseReports> PurchaseReports { get; set; }

        public DbSet<MediaFile> MediaFiles { get; set; }


    }
}