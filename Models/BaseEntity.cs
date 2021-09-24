using System;
using System.Text.Json.Serialization;

namespace angular_heroes.Models
{
    public class BaseEntity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("updatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}