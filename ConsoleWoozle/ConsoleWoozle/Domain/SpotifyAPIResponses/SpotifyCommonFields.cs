using System.Text.Json.Serialization;

namespace ConsoleWoozle.Domain.SpotifyAPIResponses
{

    public class SpotifyCommonFields
    {
        [JsonPropertyName("external_urls")]
        public List<Dictionary<string, object>>? ExternalUrls { get; set; }
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("uri")]
        public string? Uri { get; set; }
    }
    public class SpotifyCommonAlbumTrackFields : SpotifyCommonFields
    {
        [JsonPropertyName("artists")]
        public List<Dictionary<string, object>>? Artists { get; set; }

        [JsonPropertyName("available_markets")]
        public List<string>? AvailableMarkets { get; set; }
        
    }

    public class SpotifyCommonAlbumFields : SpotifyCommonFields
    {
        [JsonPropertyName("images")]
        public List<Dictionary<string, object>>? Images { get; set; }

        [JsonPropertyName("album_type")]
        public string? AlbumType { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        [JsonPropertyName("release_date_precision")]
        public string? ReleaseDatePrecision { get; set; }

        [JsonPropertyName("total_tracks")]
        public int? TotalTracks { get; set; }
    }
}
