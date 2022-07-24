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
    }
}
