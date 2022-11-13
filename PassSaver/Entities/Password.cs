namespace PassSaver.Entities
{
    public class Password
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string S_Password { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
