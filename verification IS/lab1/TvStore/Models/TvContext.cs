using Microsoft.EntityFrameworkCore;

namespace TvStore.Models
{
    public class TvContext : DbContext
    {
        public DbSet<Tv> Tv { get; set; }
        public DbSet<Order> Orders { get; set; }

        public TvContext(DbContextOptions<TvContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}