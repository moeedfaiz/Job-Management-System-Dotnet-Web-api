using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Management_System.models
{
    [Table("Users")]
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; }
    }
}
