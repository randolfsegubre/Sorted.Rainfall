using System.Runtime.Serialization.Json;
using System.Text.Json.Serialization;
using Rainfall.Data.Interfaces;

namespace Rainfall.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Item : IItem
    {
        [JsonPropertyName("@id")]
        public string id { get; set; }
        public DateTime dateTime { get; set; }
        public string measure { get; set; }
        public double value { get; set; }
    }


}
