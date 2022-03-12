using System;
using System.Text.Json.Serialization;

namespace Data.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public bool Active { get; set; }
        [JsonIgnore]
        public string Hash { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public DateTime DeleteDate { get; set; }
        [JsonIgnore]
        public DateTime UpdateDate { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
    }
}
