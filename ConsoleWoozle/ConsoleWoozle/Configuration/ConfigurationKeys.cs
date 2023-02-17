using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Configuration
{
    public static class ConfigurationKeys
    {
        public const string SpotifyCredentialsArn = "AWS:SecretsManager:CredentialsArn";
        public const string S3BucketPath = "AWS:S3:Bucket";

        public const string SpotifyAuthenticationUrl = "Spotify:Urls:AuthenticationUrl";
        public const string SpotifyBaseUrl = "Spotify:Urls:BaseUrl";

        public const string SpotifyArtistIds = "Spotify:ArtistsIds";
    }
}
