using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UrlShorter.Data;
using UrlShorter.Models;

namespace UrlShorter.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly ServerContext _context;

        public ShortUrlService(ServerContext context)
        {
            _context = context;
        }
        public ShortUrl AddShortUrl(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            return shortUrl;
        }
        public Task<ShortUrl> FindAsync(string Url) => _context.ShortUrls.FirstOrDefaultAsync(a => a.SharedUrl == Url);

        public Task SaveChangeAsync() => _context.SaveChangesAsync();
    }
}
