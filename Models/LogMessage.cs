using System.Text.Json.Serialization;

namespace angular_heroes.Models 
{
    public class LogMessage : BaseEntity
    {
        [JsonPropertyName("contents")]
        public string Contents { get; set; }
        
        [JsonIgnore]
        public User Owner { get; set; }
    }
}