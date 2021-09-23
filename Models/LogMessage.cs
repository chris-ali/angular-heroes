using System.Text.Json.Serialization;

namespace angular_heroes.Models 
{
    public class LogMessage : BaseEntity
    {
        [JsonPropertyName("contents")]
        public string Contents { get; set; }
        
        [JsonIgnore]
        public int UserIdFk { get; set; }
        
        [JsonIgnore]
        public virtual User Owner { get; set; }
    }
}