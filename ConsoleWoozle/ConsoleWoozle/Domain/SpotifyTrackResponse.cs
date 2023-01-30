using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleWoozle.Domain
{
    public class SpotifyTrackResponse : SpotifyCommonFields
    {
        [JsonPropertyName("disc_number")]
        public int? DiscNumber { get; set; }

        [JsonPropertyName("duration_ms")]
        public int? DurationMs { get; set; }

        [JsonPropertyName("explicit")]
        public bool? Explicit { get; set; }

        [JsonPropertyName("is_local")]
        public bool? IsLocal { get; set; }

        [JsonPropertyName("preview_url")]
        public string? PreviewUrl { get; set; }

        [JsonPropertyName("track_number")]
        public string? TrackNumber { get; set; }
    }
}
