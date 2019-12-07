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
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}