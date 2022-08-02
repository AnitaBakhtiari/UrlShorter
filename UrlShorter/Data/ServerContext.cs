using Microsoft.EntityFrameworkCore;
using UrlShorter.Models;

namespace UrlShorter.Data
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ShortUrl>()
                .HasIndex(b => b.SharedUrl)
                .IsUnique();

            modelBuilder.HasSequence<int>("GenerateKey", x => x.StartsAt(10).IncrementsBy(1));

            modelBuilder.Entity<ShortUrl>()
                .Property(o => o.SharedUrl)
                .HasDefaultValueSql("NEXT VALUE FOR GenerateKey");
        }

    }
}
