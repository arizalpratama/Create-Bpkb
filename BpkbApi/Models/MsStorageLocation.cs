using System.ComponentModel.DataAnnotations;

namespace BpkbApi.Models
{
    public class MsStorageLocation
    {
        [Key]
        public string LocationId { get; set; }
        [Required, MaxLength(100)]
        public string LocationName { get; set; }
    }
}
