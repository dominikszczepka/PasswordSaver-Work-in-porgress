namespace PassSaver.Exceptions
{
    public class UserNotLoggedInException : Exception
    {
        public override string Message { get; }
        public UserNotLoggedInException() : base()
        {
            Message = "User not logged in.";
        }
    }
}
