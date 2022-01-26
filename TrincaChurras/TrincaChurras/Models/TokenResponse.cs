using System;
using System.Text.Json.Serialization;

namespace TrincaChurras.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
