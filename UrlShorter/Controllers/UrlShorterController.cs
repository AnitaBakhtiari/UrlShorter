using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UrlShorter.Models;
using UrlShorter.Services;

namespace UrlShorter.Controllers
{
    [ApiController]
    public class UrlShorterController : ControllerBase
    {
        private readonly ILogger<UrlShorterController> _logger;
        private readonly IShortUrlService _service;
        public UrlShorterController(IShortUrlService service, ILogger<UrlShorterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("GenerateUrl")]
        public async Task<string> GenerateUrl(string url)
        {
            var shortUrl = new ShortUrl
            {
                BaseUrl = url
            };

            var shortUrlTable = await _service.AddShortUrl(shortUrl);

            var uriBuilder = new UriBuilder()
            {
                Scheme = "https",
                Host = "localhost",
                Port = 44336,
                Path = shortUrlTable.SharedUrl
            };
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
