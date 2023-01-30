using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleWoozle.Domain
{
    public class SpotifyArtistAlbumsResponse : SpotifyCommonAlbumFields
    {
        [JsonPropertyName("album_group")]
        public string? AlbumGroup { get; set; }
    }
}
