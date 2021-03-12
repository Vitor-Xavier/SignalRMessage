using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SignalRMessage.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
