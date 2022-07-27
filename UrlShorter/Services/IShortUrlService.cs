using System.Threading.Tasks;
using UrlShorter.Models;

namespace UrlShorter.Services
{
    public interface IShortUrlService
    {
        ShortUrl AddShortUrl(ShortUrl shortUrl);
        Task<ShortUrl> Find(string Url);
        Task SaveChange();
    }
}
