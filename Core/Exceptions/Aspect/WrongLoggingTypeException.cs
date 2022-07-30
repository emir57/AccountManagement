namespace Core.Exceptions.Aspect
{
    public class WrongLoggingTypeException : Exception
    {
        public WrongLoggingTypeException() : base("Wrong logging type")
        {
        }

        public WrongLoggingTypeException(string? message) : base(message)
        {
        }

        public WrongLoggingTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
