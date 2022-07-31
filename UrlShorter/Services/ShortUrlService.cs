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
        public async Task<ShortUrl> AddShortUrl(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);

            await _context.SaveChangesAsync();
            return shortUrl;
        }
        public Task<ShortUrl> FindAsync(string Url) => _context.ShortUrls.FirstOrDefaultAsync(a => a.SharedUrl == Url);

    }
}
