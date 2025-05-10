using LasmartTestTask.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace LasmartTestTask.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Point> Positions => Set<Point>();
        public DbSet<Comment> Comments => Set<Comment>();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>()
                        .HasMany(p => p.Comments)
                        .WithOne(c => c.Point)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
