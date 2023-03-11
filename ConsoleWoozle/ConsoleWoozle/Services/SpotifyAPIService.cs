using ConsoleWoozle.Configuration;
using ConsoleWoozle.Domain;
using ConsoleWoozle.Domain.SpotifyAPIResponses;
using Microsoft.AspNetCore.Authentication;
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
        public Task<SpotifyArtistResponse?> GetArtistAsync(string artistId);
        public Task<SpotifyBaseResponse?> GetArtistAlbumsAsync(string artistId);
        public Task<SpotifyAlbumResponse?> GetAlbumAsync(string albumId);
    }

    public class SpotifyAPIService : ISpotifyAPIService { 
        #region Fields
        private readonly SpotifyConfiguration? _configuration;
        private SpotifyCredentials? _spotifyCredentials;
        private readonly ISecretsManagementService _secretsManagementService;
        private readonly string QUERY_PARAMS = "?include_groups=album,single&limit=50";
        private readonly string ARTISTS = "/artists/";
        private readonly string ALBUMS = "/albums";
        private readonly string PLAYLISTS = "/playlists/";
        private readonly HttpClient _httpClient;
        private static string? _bearerToken;
        #endregion

        public SpotifyAPIService(SpotifyConfiguration configuration, ISecretsManagementService secretsManagementService,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _secretsManagementService = secretsManagementService;
            _httpClient = httpClientFactory.CreateClient(ApplicationConstants.ApplicationId);
        }

        #region GetArtist
        public Task<SpotifyArtistResponse?> GetArtistAsync(string artistId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _configuration?.BaseUrl + ARTISTS + artistId);
            return BuildRequest<SpotifyArtistResponse>(request);
        }
        #endregion

        #region GetArtistAlbums
        public Task<SpotifyBaseResponse?> GetArtistAlbumsAsync(string artistId)
        {
            HttpRequestMessage request = new(HttpMethod.Get, _configuration?.BaseUrl + ARTISTS + artistId + ALBUMS + QUERY_PARAMS);
            return BuildRequest<SpotifyBaseResponse>(request);
        }
        #endregion

        #region GetAlbum
        public Task<SpotifyAlbumResponse?> GetAlbumAsync(string albumId)
        {
            HttpRequestMessage request = new(HttpMethod.Get, _configuration?.BaseUrl + ALBUMS + "/" + albumId);
            return BuildRequest<SpotifyAlbumResponse>(request);
        }
        #endregion

        #region GetPlaylist
        public Task<> GetPlaylistAsync(string playlistUrl)
        {
            string playlistId = playlistUrl.Substring(33,56);
            HttpRequestMessage request = new(HttpMethod.Get, _configuration?.BaseUrl + PLAYLISTS + playlistId);
            return BuildRequest<SpotifyBaseResponse>(request)
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

            HttpRequestMessage request = new(HttpMethod.Post, _configuration?.AutenticationUrl);
            request.Headers.TryAddWithoutValidation("Authorization", credentialAuthorization);
            request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var deserializedResponse = await SendRequest<SpotifyAuthenticationResponse>(request).ConfigureAwait(false);

            if (deserializedResponse?.AccessToken != null && deserializedResponse.TokenType != null)
            {
                return deserializedResponse.TokenType + " " + deserializedResponse.AccessToken;
            }
            throw new Exception("Successful Responses with bad Data");
        }
        #endregion

        #region Build and Send Request
        private async Task<T?> BuildRequest<T>(HttpRequestMessage request)
        {
            if (_bearerToken== null)
            {
                _bearerToken = await GetBearerTokenAsync().ConfigureAwait(false);
            }
            request.Headers.TryAddWithoutValidation("Authorization", _bearerToken);
            return await SendRequest<T>(request).ConfigureAwait(false);
        }

        private async Task<T?> SendRequest<T>(HttpRequestMessage request)
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            return await JsonSerializer.DeserializeAsync<T>(response.Content.ReadAsStream()).ConfigureAwait(false);
        }
        #endregion

    }
}
