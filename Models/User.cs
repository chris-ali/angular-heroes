using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace angular_heroes.Models 
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonIgnore]
        public List<LogMessage> Messages { get; set; }

        [JsonIgnore]
        public List<Hero> Heroes {get; set;}

        [JsonIgnore]
        public List<HeroUser> HeroUsers { get; set; }
    }
}