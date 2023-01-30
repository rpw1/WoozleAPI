using ConsoleWoozle.Configuration;
using ConsoleWoozle.Domain;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleWoozle.Services
{

    public interface ISpotifyAuthenticationService
    {
        public Task<string> GetBearerToken();
    }
    public class SpotifyAuthenticationService : ISpotifyAuthenticationService
    {
        #region Fields
        private readonly SpotifyConfiguration? _configuration;
        private SpotifyCredentials? _spotifyCredentials;
        private readonly ISecretsManagementService _secretsManagementService;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        public SpotifyAuthenticationService(SpotifyConfiguration configuration, ISecretsManagementService secretsManagementService,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _secretsManagementService = secretsManagementService;
            _httpClientFactory = httpClientFactory;
        }

        #region GetBearerToken
        public async Task<string> GetBearerToken()
        {
            if (_configuration?.CredentialsArn != null)
            {
                _spotifyCredentials = await _secretsManagementService.GetSecret<SpotifyCredentials>(_configuration.CredentialsArn).ConfigureAwait(false);
            }
            var credentialAuthorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(_spotifyCredentials?.ClientId + ":" + _spotifyCredentials?.ClientSecret));

            var httpClient = _httpClientFactory.CreateClient(ApplicationConstants.ApplicationId);
            var request = new HttpRequestMessage(HttpMethod.Post, _configuration?.AutenticationUrl);
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + credentialAuthorization);
            request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            }); ;
            HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var deserializedResponse = await JsonSerializer.DeserializeAsync<SpotifyAuthenticationResponse>(response.Content.ReadAsStream()).ConfigureAwait(false);
            if (deserializedResponse?.AccessToken != null && deserializedResponse.TokenType != null)
            {
                return deserializedResponse.TokenType + " " + deserializedResponse.AccessToken;
            }
            throw new Exception("Successful Responses with bad Data");
        }
        #endregion
    }
}
