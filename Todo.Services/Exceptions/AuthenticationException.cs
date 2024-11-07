namespace Todo.Busines.Exceptions;

public class AuthenticationException : Exception
{
    public AuthenticationException(string? errorCode = null)
    {
        ErrorCode = errorCode;
    }

    public AuthenticationException(string message, string? errorCode = null)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public AuthenticationException(string message, Exception inner, string? errorCode = null)
        : base(message, inner)
    {
        ErrorCode = errorCode;
    }

    public string? ErrorCode { get; set; }
}
