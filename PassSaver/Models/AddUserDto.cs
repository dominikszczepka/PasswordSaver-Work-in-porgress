using System.ComponentModel.DataAnnotations;

namespace PassSaver.Models
{
    public class AddUserDto
    {
        [Required]
        [MaxLength(64)]
        public string Username { get; set; }
        [Required]
        public string UserUnhashedPassword { get; set; }
        [Required]
        public string UserEmail { get; set; }
    }
}
