using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Configuration
{
    public class SpotifyConfiguration
    {
        public string? CredentialsArn { get; set; }
        public string? AutenticationUrl { get; set;}
        public string? BaseUrl { get; set;}
    }

    public class SpotifyCredentials
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
