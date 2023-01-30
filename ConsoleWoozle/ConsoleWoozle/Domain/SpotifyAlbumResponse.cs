using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleWoozle.Domain
{
    public class SpotifyAlbumResponse : SpotifyCommonAlbumFields
    {
        [JsonPropertyName("copyrights")]
        public List<Dictionary<string, object>>? Copyrights { get; set; }

        [JsonPropertyName("external_ids")]
        public Dictionary<string, object>? ExternalIds { get; set; }

        [JsonPropertyName("genres")]
        public List<string>? Genres { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("popularity")]
        public int? Popularity { get; set; }

        [JsonPropertyName("restrictions")]
        public Dictionary<string,object>? Restrictions { get; set; }

        [JsonPropertyName("tracks")]
        public SpotifyBaseResponse? Tracks { get; set; }
    }
}
