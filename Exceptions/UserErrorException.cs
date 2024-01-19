namespace DIS_project.Exceptions
{
    public class UserErrorException : Exception
    {
        public UserErrorException(string? message) : base(message)
        {
        }
    }
}
