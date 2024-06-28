using ComicLoreApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicLoreApi.DbContexts
{
    public class SupeInfoDbContext : DbContext
    {
        public SupeInfoDbContext(DbContextOptions<SupeInfoDbContext> options) : base(options)
        {
        }

        public DbSet<Supe> Supes { get; set; }
        public DbSet<Power> Powers { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Supe>()
                .HasMany(s => s.Powers)
                .WithMany(p => p.Supes);

            // Seed initial data
            modelBuilder.Entity<Supe>().HasData(
                new Supe { Id = 1, FirstName = "Clark", LastName = "Kent", Alias = "Superman", DateOfBirth = new DateTime(1938, 6, 1), Origin = "Krypton" },
                new Supe { Id = 2, FirstName = "Bruce", LastName = "Wayne", Alias = "Batman", DateOfBirth = new DateTime(1939, 5, 1), Origin = "Gotham City" },
                new Supe { Id = 3, FirstName = "Diana", LastName = "Prince", Alias = "Wonder Woman", DateOfBirth = new DateTime(1941, 12, 1), Origin = "Themyscira" }
            );

            modelBuilder.Entity<Power>().HasData(
                new Power { Id = 1, Name = "Flight", Description = "Ability to fly", PowerTier = "A" },
                new Power { Id = 2, Name = "Super Strength", Description = "Enhanced physical strength", PowerTier = "S" },
                new Power { Id = 3, Name = "Invisibility", Description = "Ability to become invisible", PowerTier = "B" },
                new Power { Id = 4, Name = "Gadgets", Description = "Use of advanced technology and gadgets", PowerTier = "C" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "admin", PassWord = "admin", Role = "Admin" },
                               new User { Id = 2, UserName = "user", PassWord = "user", Role = "User" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
