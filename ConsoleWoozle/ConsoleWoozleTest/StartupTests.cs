using ConsoleWoozle;
using ConsoleWoozle.Services;
using Xunit;

namespace ConsoleWoozleTest
{
    public class StartupTests
    {
        private readonly Startup _startup = new Startup();

        public StartupTests()
        {
            _startup.ConfigureServices();
        }

        [Fact]
        public void TestGetMethod()
        {
            var secretsManagementService = _startup.GetSecretsManagementService();
            Assert.True(secretsManagementService != null);

            var spotifyAPIService = _startup.GetSpotifyAPIService();
            Assert.True(spotifyAPIService != null);

        }
    }
}