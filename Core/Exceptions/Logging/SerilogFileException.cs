namespace Core.Exceptions.Logging
{
    public class SerilogFilePathException : Exception
    {
        public SerilogFilePathException() : base("File path not found")
        {
        }

        public SerilogFilePathException(string? message) : base(message)
        {
        }

        public SerilogFilePathException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
