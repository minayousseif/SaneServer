using System.ComponentModel.DataAnnotations;

namespace SaneServer.Server.Models
{
    public class Scanner
    {
        [Key, Required]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [StringLength(100), MaxLength(100)]
        public string FriendlyName { get; set; }
        [StringLength(100), MaxLength(100)]
        public string Vendor { get; set; }
        [StringLength(100), MaxLength(100)]
        public string Model { get; set; }

    }
}