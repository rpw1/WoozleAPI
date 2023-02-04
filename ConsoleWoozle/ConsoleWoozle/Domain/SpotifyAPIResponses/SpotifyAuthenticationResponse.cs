﻿using System.Text.Json.Serialization;

namespace ConsoleWoozle.Domain.SpotifyAPIResponses
{
    public class SpotifyAuthenticationResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public string? ExpiresIn { get; set; }
    }
}
