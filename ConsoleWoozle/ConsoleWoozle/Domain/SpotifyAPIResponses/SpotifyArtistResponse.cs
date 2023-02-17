using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleWoozle.Domain.SpotifyAPIResponses
{
    public class SpotifyArtistResponse : SpotifyCommonFields
    {
        [JsonPropertyName("popularity")]
        public int? Popularity { get; set; }

        [JsonPropertyName("followers")]
        public Dictionary<string, object>? Followers { get; set; }
        [JsonPropertyName("genres")]
        public Dictionary<string, object>? Genres { get; set; }
        [JsonPropertyName("images")]
        public List<Dictionary<string, object>>? Images { get; set; }
    }
}
