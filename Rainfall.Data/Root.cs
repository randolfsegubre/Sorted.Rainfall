using Rainfall.Data.Interfaces;
using System.Net;
using System.Text.Json.Serialization;

namespace Rainfall.Data
{
    public class Root : IRoot
    {
        [JsonPropertyName("@context")]
        public string? Context { get; set; }
        public Meta? meta { get; set; }
        public List<Item>? items { get; set; }
    }


}
