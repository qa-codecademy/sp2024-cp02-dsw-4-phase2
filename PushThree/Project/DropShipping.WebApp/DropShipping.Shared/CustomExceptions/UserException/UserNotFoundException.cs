namespace DropShipping.Shared.CustomExceptions.UserException
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}
