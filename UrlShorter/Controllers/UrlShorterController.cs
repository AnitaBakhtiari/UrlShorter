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
            var random = Helper.GenerateRandomString();
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


        [HttpGet("{value}")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "value" })]
        public async Task<IActionResult> RedirectUrl(string value)
        {
            var url = await _service.FindAsync(value);
            if (url is null) return NotFound();
            return Redirect(url.BaseUrl);
        }

       
    }
}
