namespace PassSaver.Exceptions
{
    public class UserNotFoundException : Exception 
    {
        public override string Message { get; }
        public UserNotFoundException() : base()
        {
            Message = "User not found.";
        }
    }
}
