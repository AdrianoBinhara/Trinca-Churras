using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrincaChurras.Models
{
    public class Barbecue
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("value_per_person")]
        public double Value_per_person { get; set; }

        [JsonPropertyName("participants_count")]
        public int Participants_count { get; set; }

        public string Image { get; set; }

        [JsonPropertyName("participants")]
        public List<Person> Participants { get; set; }

        public double TotalValue
        {
            get {return  Participants_count* Value_per_person;}
        }
    }
}
