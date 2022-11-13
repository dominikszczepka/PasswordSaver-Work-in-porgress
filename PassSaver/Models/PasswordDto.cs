using PassSaver.Entities;

namespace PassSaver.Models
{
    public class PasswordDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string S_Password { get; set; }
        public int UserId { get; set; }
    }
}
