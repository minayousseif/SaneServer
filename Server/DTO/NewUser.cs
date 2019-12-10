using System.ComponentModel.DataAnnotations;

namespace SaneServer.Server.DTO
{
    public class NewUser : UserDTO
    {
        [Required, DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public override string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}