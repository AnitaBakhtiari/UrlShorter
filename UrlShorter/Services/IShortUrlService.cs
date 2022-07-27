using System.Threading.Tasks;
using UrlShorter.Models;

namespace UrlShorter.Services
{
    public interface IShortUrlService
    {
        ShortUrl AddShortUrl(ShortUrl shortUrl);
        Task<ShortUrl> FindAsync(string Url);
        Task SaveChangeAsync();
    }
}
