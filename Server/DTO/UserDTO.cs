using System.ComponentModel.DataAnnotations;

namespace SaneServer.Server.DTO
{
    public class UserDTO
    {
        [Required]
        [StringLength(120)]
        public string UserName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public virtual string Password { get; set; }
    }
}