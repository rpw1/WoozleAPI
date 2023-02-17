

using ConsoleWoozle.Domain.SpotifyAPIResponses;
using ConsoleWoozle.Services;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleWoozleTest.Mocks
{
    public class MockSpotifyAPIService : ISpotifyAPIService
    {
        public Task<SpotifyAlbumResponse> GetAlbumAsync(string albumId)
        {
            if (albumId == "SOS")
            {
                return LoadTestData<SpotifyAlbumResponse>("SZASOSAlbum.json");
            }
            else if (albumId == "Nice")
            {
                return LoadTestData<SpotifyAlbumResponse>("WoozyIfThisIsntNiceAlbum.json");
            }
            else if (albumId == "Kenny")
            {
                return LoadTestData<SpotifyAlbumResponse>("WoozyKennySingle.json");
            }
            else if (albumId == "Pool")
            {
                return LoadTestData<SpotifyAlbumResponse>("WoozyPoolSingle.json");
            }
            else if (albumId == "Cheesin")
            {
                return LoadTestData<SpotifyAlbumResponse>("CautiousClayCheesinSingle.json");
            }
            return null;
        }

        public Task<SpotifyArtistResponse> GetArtistAsync(string artistId)
        {
            if (artistId == "Billie")
            {
                return LoadTestData<SpotifyArtistResponse>("BillieEilishArtist.json");
            }
            else if (artistId == "SZA")
            {
                return LoadTestData<SpotifyArtistResponse>("SZAArtist.json");
            }
            else if (artistId == "Woozy")
            {
                return LoadTestData<SpotifyArtistResponse>("StillWoozyArtist.json");
            }
            return null;
        }

        public Task<SpotifyBaseResponse> GetArtistAlbumsAsync(string artistId)
        {
            if (artistId == "Billie")
            {
                return LoadTestData<SpotifyBaseResponse>("BillieArtistAlbums.json");
            }
            else if (artistId == "SZA")
            {
                return LoadTestData<SpotifyBaseResponse>("SZAArtistAlbums.json");
            }
            else if(artistId == "Woozy")
            {
                return LoadTestData<SpotifyBaseResponse>("WoozyArtistAlbums.json");
            }
            return null;
        }

        private async Task<T> LoadTestData<T>(string filePath)
        {
            using StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + @"/TestData/" + filePath);
            string json = await r.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
