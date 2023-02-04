using System.Text.Json.Serialization;

namespace ConsoleWoozle.Domain.SpotifyAPIResponses
{
    public class SpotifyArtistAlbumsResponse : SpotifyCommonAlbumFields
    {
        [JsonPropertyName("album_group")]
        public string? AlbumGroup { get; set; }
    }
}
