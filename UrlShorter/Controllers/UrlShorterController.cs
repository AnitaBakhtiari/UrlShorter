using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlShorter.Data;
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
            var uriBuilder = new UriBuilder()
            {
                Scheme = "https",
                Host = "localhost",
                Port = 44336,
                Path = Helper.GenerateRandomString()
            };

            var shortUrl = new ShortUrl
            {
                BaseUrl = url,
                SharedUrl = uriBuilder.ToString()
            };

            _service.AddShortUrl(shortUrl);
            await _service.SaveChange();

            return uriBuilder.ToString();

        }


        [HttpGet("{value}")]
        public async Task<IActionResult> RedirectUrl(string value)
        {
            var url = await _service.Find(value);
            if (url is null) return NotFound();
            return Redirect(url.BaseUrl);
        }

       
    }
}
