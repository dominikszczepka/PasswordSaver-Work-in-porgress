namespace PassSaver.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserHashedPassword { get; set; }
        public string UserEmail { get; set; }
        public virtual List<Password> Passwords { get; set; }
    }
}
