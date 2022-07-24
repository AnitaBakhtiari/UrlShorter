using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UrlShorter.Data;
using UrlShorter.Models;

namespace UrlShorter.Controllers
{
    [ApiController]
    public class UrlShorterController : ControllerBase
    {
        private readonly ServerContext _context;
        public UrlShorterController(ServerContext context)
        {
            _context = context;
        }

        [HttpPost("GenerateUrl")]
        public string GenerateUrl(string url)
        {
            var uriBuilder = new UriBuilder()
            {
                Scheme = "https",
                Host = "localhost",
                Port = 44336,
                Path = GenerateRandomString()
            };

            var shortUrl = new ShortUrl
            {
                Id = new Guid(),
                BaseUrl = url,
                SharedUrl = uriBuilder.ToString()
            };

            _context.ShortUrls.Add(shortUrl);
            _context.SaveChanges();

            return uriBuilder.ToString();

        }


        [HttpGet("{value}")]
        public IActionResult RedirectUrl(string value)
        {
            var url = _context.ShortUrls.FirstOrDefault(a => a.SharedUrl.Contains(value));
            if (url is null) return NotFound();
            return Redirect(url.BaseUrl);
        }

        private static string GenerateRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new string(stringChars);

            return finalString;
        }
    }
}
