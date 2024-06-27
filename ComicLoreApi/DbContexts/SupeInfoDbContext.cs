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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Supe>()
                .HasMany(s => s.Powers)
                .WithMany(p => p.Supes);

            base.OnModelCreating(modelBuilder);
        }
    }
}
