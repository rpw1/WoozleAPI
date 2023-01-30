using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Configuration
{
    public static class ConfigurationKeys
    {
        public const string SpotifyCredentialsArn = "SecretsManager:CredentialsArn";

        public const string SpotifyAuthenticationUrl = "SpotifyUrls:AuthenticationUrl";
        public const string SpotifyBaseUrl = "SpotifyUrls:BaseUrl";
    }
}
