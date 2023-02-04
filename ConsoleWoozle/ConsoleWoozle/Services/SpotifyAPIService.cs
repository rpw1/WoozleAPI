using ConsoleWoozle.Configuration;
using ConsoleWoozle.Domain;
using ConsoleWoozle.Domain.SpotifyAPIResponses;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleWoozle.Services
{
    public interface ISpotifyAPIService
    {
        public Task<SpotifyBaseResponse?> GetArtist(string artistId);
        public Task<SpotifyAlbumResponse?> GetAlbum(string albumId);
    }

    public class SpotifyAPIService : ISpotifyAPIService
    {
        #region Fields
        private readonly SpotifyConfiguration? _configuration;
        private SpotifyCredentials? _spotifyCredentials;
        private readonly ISecretsManagementService _secretsManagementService;
        private readonly string QUERY_PARAMS = "?include_groups=album,single&limit=50";
        private readonly string ARTISTS = "/artists/";
        private readonly string ALBUMS = "/albums";
        private readonly HttpClient _httpClient;
        #endregion

        public SpotifyAPIService(SpotifyConfiguration configuration, ISecretsManagementService secretsManagementService,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _secretsManagementService = secretsManagementService;
            _httpClient = httpClientFactory.CreateClient(ApplicationConstants.ApplicationId);
        }

        #region GetArtist
        public async Task<SpotifyBaseResponse?> GetArtist(string artistId)
        {
            string authenticationToken = await GetBearerTokenAsync().ConfigureAwait(false);

            var request = new HttpRequestMessage(HttpMethod.Get, _configuration?.BaseUrl + ARTISTS + artistId + ALBUMS + QUERY_PARAMS);
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + authenticationToken);

            HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<SpotifyBaseResponse>(response.Content.ReadAsStream()).ConfigureAwait(false);
        }
        #endregion

        #region GetAlbum
        public async Task<SpotifyAlbumResponse?> GetAlbum(string albumId)
        {
            string authenticationToken = await GetBearerTokenAsync().ConfigureAwait(false);

            var request = new HttpRequestMessage(HttpMethod.Get, _configuration?.BaseUrl + ALBUMS + "/" + albumId);
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + authenticationToken);

            HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await JsonSerializer.DeserializeAsync<SpotifyAlbumResponse>(response.Content.ReadAsStream()).ConfigureAwait(false);
        }
        #endregion

        #region GetBearerToken
        private async Task<string> GetBearerTokenAsync()
        {
            if (_configuration?.CredentialsArn != null)
            {
                _spotifyCredentials = await _secretsManagementService.GetSecretAsync<SpotifyCredentials>(_configuration.CredentialsArn).ConfigureAwait(false);
            }
            var credentialAuthorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(_spotifyCredentials?.ClientId + ":" + _spotifyCredentials?.ClientSecret));

            var request = new HttpRequestMessage(HttpMethod.Post, _configuration?.AutenticationUrl);
            request.Headers.TryAddWithoutValidation("Authorization", credentialAuthorization);
            request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            }); ;
            HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);

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
