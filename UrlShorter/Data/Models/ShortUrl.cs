using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShorter.Models
{
    public class ShortUrl
    {
        [Key]
        public long Id { get; set; }
        public string BaseUrl { get; set; }
  
        public string SharedUrl { get; set; }
    }
}
