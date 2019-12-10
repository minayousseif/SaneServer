using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaneServer.Server.Models
{
    public enum UserRole {
        Admin, User
    }
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string UserName { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [Required, MaxLength(1024)]
        public string PasswordHash { get; set; }
        public virtual UserRole Role {get; set;}

        public string RoleString {
            get {
                return Enum.GetName(typeof(UserRole), Role);
            }
        }
    }
}