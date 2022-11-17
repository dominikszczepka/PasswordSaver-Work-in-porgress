namespace PassSaver.Exceptions
{
    public class UserNotFoundException : Exception 
    {
        string Message { get; set; }
        public UserNotFoundException() : base()
        {
            Message = "User not found.";
        }
    }
}
