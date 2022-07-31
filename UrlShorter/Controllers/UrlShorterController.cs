using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UrlShorter.Models;
using UrlShorter.Services;
using UrlShorter.Utils;

namespace UrlShorter.Controllers
{
    [ApiController]
    public class UrlShorterController : ControllerBase
    {
        private readonly IShortUrlService _service;
        public UrlShorterController(IShortUrlService service)
        {
            _service = service;
        }

        [HttpPost("GenerateUrl")]
        public async Task<string> GenerateUrl(string url)
        {
            //Create random string
            var random = Util.GenerateRandomString();

            //Create short url for users
            var uriBuilder = new UriBuilder()
            {
                Scheme = "https",
                Host = "localhost",
                Port = 44336,
                Path = random
            };

            var shortUrl = new ShortUrl
            {
                BaseUrl = url,
                SharedUrl = random
            };

            await _service.AddShortUrl(shortUrl);

            return uriBuilder.ToString();

        }


        [HttpGet("{randomId}")]
        [ResponseCache(Duration = 120 * 60, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "randomId" })]
        public async Task<IActionResult> RedirectUrl(string randomId)
        {
            var url = await _service.FindAsync(randomId);
            if (url is null) return NotFound();
            return Redirect(url.BaseUrl);
        }


    }
}
