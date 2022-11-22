using PassSaver.Entities;

namespace PassSaver.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserHashedPassword { get; set; }
        public string UserEmail { get; set; }
        public virtual List<PasswordDto> Passwords { get; set; }
    }
}
