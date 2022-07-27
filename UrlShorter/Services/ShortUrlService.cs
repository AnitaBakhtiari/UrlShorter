using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UrlShorter.Data;
using UrlShorter.Models;

namespace UrlShorter.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly ServerContext _context;

        public ShortUrl AddShortUrl(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            return shortUrl;
        }

        public async Task<ShortUrl> Find(string Url) => await _context.ShortUrls.FirstOrDefaultAsync(a => a.SharedUrl.Contains(Url));

        public async Task SaveChange() => await _context.SaveChangesAsync();
    }
}
