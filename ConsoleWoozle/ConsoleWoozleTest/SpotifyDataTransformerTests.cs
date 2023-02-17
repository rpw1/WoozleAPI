﻿using ConsoleWoozle.Domain.SpotifyAPIResponses;
using ConsoleWoozle.Services;
using ConsoleWoozle.Utils;
using ConsoleWoozleTest.Mocks;
using Newtonsoft.Json;
using System.Diagnostics;
using Xunit;

namespace ConsoleWoozleTest
{
    public class SpotifyDataTransformerTests
    {
        private readonly ISpotifyAPIService _spotifyAPIService;

        public SpotifyDataTransformerTests()
        {
            _spotifyAPIService = new MockSpotifyAPIService();
        }

        [Fact]
        public async void Test_Artist_Transform()
        {
            SpotifyBaseResponse artistResponse = await _spotifyAPIService.GetArtistAlbumsAsync("Woozy").ConfigureAwait(false);
            var transformedData = SpotifyDataTransformer.TransformBaseResponse(artistResponse.Href, artistResponse);
            
            SpotifyAlbumResponse nice = await _spotifyAPIService.GetAlbumAsync("Nice").ConfigureAwait(false);
            transformedData.TransformAndAddAlbumResponse(nice);

            SpotifyAlbumResponse kenny = await _spotifyAPIService.GetAlbumAsync("Kenny").ConfigureAwait(false);
            transformedData.TransformAndAddAlbumResponse(kenny);

            SpotifyAlbumResponse pool = await _spotifyAPIService.GetAlbumAsync("Pool").ConfigureAwait(false);
            transformedData.TransformAndAddAlbumResponse(pool);

            Assert.Equal(18, transformedData.Albums.Count);
            Assert.Equal(14, transformedData.Songs.Count);
        }
    }
}
