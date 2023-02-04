

using ConsoleWoozle.Domain.SpotifyAPIResponses;
using ConsoleWoozle.Services;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleWoozleTest.Mocks
{
    public class MockSpotifyAPIService : ISpotifyAPIService
    {
        public Task<SpotifyAlbumResponse> GetAlbum(string albumId)
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

        public Task<SpotifyBaseResponse> GetArtist(string artistId)
        {
            if (artistId == "Billie")
            {
                return LoadTestData<SpotifyBaseResponse>("BillieArtist.json");
            }
            else if (artistId == "SZA")
            {
                return LoadTestData<SpotifyBaseResponse>("SZAArtist.json");
            }
            else if(artistId == "Woozy")
            {
                return LoadTestData<SpotifyBaseResponse>("WoozyArtist.json");
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
