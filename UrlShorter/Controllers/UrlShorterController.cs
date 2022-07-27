using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UrlShorter.Helpers;
using UrlShorter.Models;
using UrlShorter.Services;

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
            var random = Helper.GenerateRandomString();

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

            _service.AddShortUrl(shortUrl);
            await _service.SaveChangeAsync();

            return uriBuilder.ToString();

        }


        [HttpGet("{randomId}")]
        [ResponseCache(Duration = 120, VaryByQueryKeys = new string[] { "randomId" })]
        public async Task<IActionResult> RedirectUrl(string randomId)
        {
            var url = await _service.FindAsync(randomId);
            if (url is null) return NotFound();
            return Redirect(url.BaseUrl);
        }


    }
}
