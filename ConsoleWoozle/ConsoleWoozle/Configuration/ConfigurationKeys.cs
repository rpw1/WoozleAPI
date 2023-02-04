using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Configuration
{
    public static class ConfigurationKeys
    {
        public const string SpotifyCredentialsArn = "Spotify:SecretsManager:CredentialsArn";

        public const string SpotifyAuthenticationUrl = "Spotify:Urls:AuthenticationUrl";
        public const string SpotifyBaseUrl = "Spotify:Urls:BaseUrl";

        public const string SpotifyArtistIds = "Spotify:ArtistsIds";
    }
}
