using System;
using System.Text.Json.Serialization;

namespace TrincaChurras.Models
{
    public class Person
    {
        [JsonPropertyName("bbq_id")]
        public string Bbq_id { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("confirmed")]
        public bool Confirmed { get; set; }

        [JsonPropertyName("value_paid")]
        public double Value_paid { get; set; }
    }
}
