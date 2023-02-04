using ConsoleWoozle.Domain;
using ConsoleWoozle.Domain.SpotifyAPIResponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Utils
{
    public static class SpotifyDataTransformer
    {
        public static SavedArtist TransformBaseResponse(string artistId, SpotifyBaseResponse response)
        {
            SavedArtist result = new(artistId);
            foreach (var item in response.Items ?? new List<Dictionary<string, object>>())
            {
                var albumResponse = JsonConvert.DeserializeObject<SpotifyAlbumResponse>(JsonConvert.SerializeObject(item));
                result.Albums.Add(new SavedAlbum
                {
                    Id = albumResponse?.Id,
                    Name = albumResponse?.Name,
                    Type = albumResponse?.Type,
                });
            }
            return result;
        }

        public static void TransformAndAddAlbumResponse(this SavedArtist artist, SpotifyAlbumResponse response)
        {
            var baseResponse = JsonConvert.DeserializeObject<SpotifyBaseResponse>(JsonConvert.SerializeObject(response?.Tracks));
            foreach (var item in baseResponse?.Items ?? new List<Dictionary<string, object>>())
            {
                var track = JsonConvert.DeserializeObject<SpotifyTrackResponse>(JsonConvert.SerializeObject(item));
                string songName;
                if (track?.Name == null)
                {
                    continue;
                } else
                {
                    songName = track.Name;
                }
                SavedSong savedSong = new()
                {
                    Id = track?.Id,
                    Name = songName,
                    Explicit = track?.Explicit,
                    Href = track?.Href,
                    Uri = track?.Uri,
                };
                if (!artist.Songs.ContainsKey(songName))
                {
                    artist.Songs[songName] = savedSong;
                } else if (!savedSong.Explicit ?? false)
                {
                    artist.Songs[songName] = savedSong;
                }
            }
        }
    }
}
