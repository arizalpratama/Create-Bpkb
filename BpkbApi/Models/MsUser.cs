using System.ComponentModel.DataAnnotations;

namespace BpkbApi.Models
{
    public class MsUser
    {
        [Key]
        public long UserId { get; set; }
        [Required, MaxLength(20)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
