namespace Imagizer.Api.Exceptions;

public class InvalidSizeException : Exception
{
    public InvalidSizeException()
    {
    }

    public InvalidSizeException(string message) : base(message)
    {
    }

    public InvalidSizeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}