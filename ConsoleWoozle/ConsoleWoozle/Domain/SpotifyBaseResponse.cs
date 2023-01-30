using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleWoozle.Domain
{
    public class SpotifyBaseResponse
    {
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("items")]
        public List<Dictionary<string, object>>? Items { get; set; }

        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        [JsonPropertyName("next")]
        public string? Next { get; set; }

        [JsonPropertyName("offset")]
        public int? Offset { get; set; }

        [JsonPropertyName("previous")]
        public string? Previous { get; set; }

        [JsonPropertyName("total")]
        public int? Total { get; set; }
    }
}
