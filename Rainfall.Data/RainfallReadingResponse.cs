using Rainfall.Data.Interfaces;
using System.Text.Json.Serialization;

namespace Rainfall.Data
{
    public class RainfallReadingResponse : IRainfallReadingResponse
    {
        [JsonPropertyName("@context")]
        public string context { get; set; }
        public Meta meta { get; set; }
        public List<Item> items { get; set; }
    }


}
