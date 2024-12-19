using System.Text.Json.Serialization;

namespace BpkbMvc.Models
{
    public class MsStorageLocation
    {
        //[JsonPropertyName("locationId")]
        public string LocationId { get; set; }
        //[JsonPropertyName("locationName")]
        public string LocationName { get; set; }
    }
}
